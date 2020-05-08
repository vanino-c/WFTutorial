using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esoft_Project
{
    public partial class FormDeal : Form
    {
        public FormDeal()
        {
            InitializeComponent();
            ShowSupply();
            ShowDemand();
            ShowDealSet();
        }

        void ShowSupply()
        {
            //Очищаем comboBox
            comboBoxSupply.Items.Clear();
            foreach (SupplySet supplySet in Program.wftDB.SupplySet)
            {
                //Добавляем Предложение с нужной нам информацией(ID, Фамилия риелтора + инициалы, Фамилия клиента + инициалы)
                string[] item =
                {
                    supplySet.Id.ToString() + ".",
                    "Риелтор: " + supplySet.AgentsSet.LastName + " " +  supplySet.AgentsSet.FirstName.Substring(0, 1) + "." +  supplySet.AgentsSet.MiddleName.Substring(0, 1) + ".",
                    "Клиент: " + supplySet.ClientSet.LastName + " " +  supplySet.ClientSet.FirstName.Substring(0, 1) + "." +  supplySet.ClientSet.MiddleName.Substring(0, 1) + "."
                };
                comboBoxSupply.Items.Add(string.Join(" ", item));
            }
        }
        void ShowDemand()
        {
            //Очищаем comboBox
            comboBoxDemand.Items.Clear();
            foreach (DemandSet demand in Program.wftDB.DemandSet)
            {
                //Добавляем Потребность с нужной нам информацией(ID, Фамилия риелтора + инициалы, Фамилия клиента + инициалы)
                string[] item =
                {
                    demand.Id.ToString() + ".",
                    "Риелтор: " + demand.AgentsSet.LastName + " " +  demand.AgentsSet.FirstName.Substring(0, 1) + "." +  demand.AgentsSet.MiddleName.Substring(0, 1) + ".",
                    "Клиент: " + demand.ClientSet.LastName + " " +  demand.ClientSet.FirstName.Substring(0, 1) + "." +  demand.ClientSet.MiddleName.Substring(0, 1) + "."
                };
                comboBoxDemand.Items.Add(string.Join(" ", item));
            }
        }

        private void comboBoxSupply_SelectedIndexChanged(object sender, EventArgs e)
        {
            Deductions();
        }

        private void comboBoxDemand_SelectedIndexChanged(object sender, EventArgs e)
        {
            Deductions();
        }

        void Deductions()
        {
            if (comboBoxSupply.SelectedItem != null && comboBoxDemand.SelectedItem != null)
            {
                //Находим в базе предложение и потребность с выбраными номерами
                SupplySet supplySet = Program.wftDB.SupplySet.Find(Convert.ToInt32(comboBoxSupply.SelectedItem.ToString().Split('.')[0]));
                DemandSet demandSet = Program.wftDB.DemandSet.Find(Convert.ToInt32(comboBoxDemand.SelectedItem.ToString().Split('.')[0]));
                //Расчитываем отчисления компании для клиента-покупателя (3% от стоимости) и выводим в textBoxCustomerCompanyDeductions
                double customerCompanyDeductions = supplySet.Price * 0.03;
                textBoxCustomerCompanyDeductions.Text = customerCompanyDeductions.ToString("0.00");
                //Расчитываем отчисления риелтору для клиента-покупателя (комиссия указана в AgentsSet) и выводим в textBoxAgentCustomerDeductions
                if (demandSet.AgentsSet.Share != 0)
                {
                    double AgentCustomerDeductions = customerCompanyDeductions * Convert.ToDouble(demandSet.AgentsSet.Share) / 100.00;
                    textBoxAgentCustomerDeductions.Text = AgentCustomerDeductions.ToString("0.00");
                }
                //Если же комиссия не указана, берем 45%
                else
                {
                    double AgentCustomerDeductions = customerCompanyDeductions * 0.45;
                    textBoxAgentCustomerDeductions.Text = AgentCustomerDeductions.ToString("0.00");
                }
            }
            else
            {
                textBoxCustomerCompanyDeductions.Text = "";
                textBoxAgentCustomerDeductions.Text = "";
            }
            if (comboBoxSupply.SelectedItem != null)
            {
                //Находим в базе предложение c выбраным номером
                SupplySet supplySet = Program.wftDB.SupplySet.Find(Convert.ToInt32(comboBoxSupply.SelectedItem.ToString().Split('.')[0]));
                //Расчитываем отчисления компании для клиента-продавца
                double sellerCompanyDeductions;
                //Если квартира
                if (supplySet.RealEstateSet.Type == 0)
                {
                    sellerCompanyDeductions = 36000 + supplySet.Price * 0.01;
                    textBoxSellerCompanyDeductions.Text = sellerCompanyDeductions.ToString("0.00");
                }
                //Если дом
                else if (supplySet.RealEstateSet.Type == 1)
                {
                    sellerCompanyDeductions = 30000 + supplySet.Price * 0.01;
                    textBoxSellerCompanyDeductions.Text = sellerCompanyDeductions.ToString("0.00");
                }
                //Если земля
                else
                {
                    sellerCompanyDeductions = 30000 + supplySet.Price * 0.02;
                    textBoxSellerCompanyDeductions.Text = sellerCompanyDeductions.ToString("0.00");
                }
                //Расчитываем отчисления риелтору для клиента-продавца (комиссия указана в AgentsSet) и выводим в textBoxAgentSellerDeductions
                if (supplySet.AgentsSet.Share != 0)
                {
                    double AgentSellerDeductions = sellerCompanyDeductions * Convert.ToDouble(supplySet.AgentsSet.Share) / 100.00;
                    textBoxAgentSellerDeductions.Text = AgentSellerDeductions.ToString("0.00");
                }
                //Если же комиссия не указана, берем 45%
                else
                {
                    double AgentSellerDeductions = sellerCompanyDeductions * 0.45;
                    textBoxAgentSellerDeductions.Text = AgentSellerDeductions.ToString("0.00");
                }
            }
            else
            {
                textBoxCustomerCompanyDeductions.Text = "";
                textBoxAgentCustomerDeductions.Text = "";
                textBoxAgentSellerDeductions.Text = "";
                textBoxSellerCompanyDeductions.Text = "";
            }
        }
    
        void ShowDealSet()
        {
            //Очищаем listview
            listViewDealSet.Items.Clear();
            foreach (DealSet dealSet in Program.wftDB.DealSet)
            {
                //Новый элемент из масива строк
                ListViewItem item = new ListViewItem(new string[]
                {
                    //Фамилия и инициалы клиента-продавца
                    dealSet.SupplySet.ClientSet.LastName + " " +  dealSet.SupplySet.ClientSet.FirstName.Substring(0, 1) + "." +  dealSet.SupplySet.ClientSet.MiddleName.Substring(0, 1) + ".",
                    //Фамилия и инициалы риелтора клиента-продавца
                    dealSet.SupplySet.AgentsSet.LastName + " " +  dealSet.SupplySet.AgentsSet.FirstName.Substring(0, 1) + "." +  dealSet.SupplySet.AgentsSet.MiddleName.Substring(0, 1) + ".",
                    //Фамилия и инициалы клиента-покупателя
                    dealSet.DemandSet.ClientSet.LastName + " " +  dealSet.DemandSet.ClientSet.FirstName.Substring(0, 1) + "." +  dealSet.DemandSet.ClientSet.MiddleName.Substring(0, 1) + ".",
                    //Фамилия и инициалы риелтора клиента-покупателя
                    dealSet.DemandSet.AgentsSet.LastName + " " +  dealSet.DemandSet.AgentsSet.FirstName.Substring(0, 1) + "." +  dealSet.DemandSet.AgentsSet.MiddleName.Substring(0, 1) + ".",
                    //Адресс недвижимости
                    "г. " + dealSet.SupplySet.RealEstateSet.Address_City + ", ул. " +
                    dealSet.SupplySet.RealEstateSet.Address_Street + ", д. " + dealSet.SupplySet.RealEstateSet.Address_House +
                    " кв. " + dealSet.SupplySet.RealEstateSet.Address_Number + " площадь " + dealSet.SupplySet.RealEstateSet.TotalArea,
                    //Цена
                    dealSet.SupplySet.Price.ToString()
                });
                //Указываем тег
                item.Tag = dealSet;
                //Добавляем в listViewDealSet
                listViewDealSet.Items.Add(item);
            }
            listViewDealSet.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Проверяем заполненость списков и полей
            if (comboBoxDemand.SelectedItem != null && comboBoxSupply.SelectedItem != null
                && textBoxAgentCustomerDeductions.Text != "" && textBoxAgentSellerDeductions.Text != ""
                && textBoxCustomerCompanyDeductions.Text != "" && textBoxSellerCompanyDeductions.Text != "")
            {
                //Новый экземпляр класса Сделка
                DealSet dealSet = new DealSet();
                //Отделяем ID потребности и предложения
                dealSet.IdSupply = Convert.ToInt32(comboBoxSupply.SelectedItem.ToString().Split('.')[0]);
                dealSet.IdDemand = Convert.ToInt32(comboBoxDemand.SelectedItem.ToString().Split('.')[0]);
                //Добавляем dealSet в таблицу
                Program.wftDB.DealSet.Add(dealSet);
                //Сохраняем изменения
                Program.wftDB.SaveChanges();
                ShowDealSet();
            }
            else MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //Если в listView выбран элемент
            if (listViewDealSet.SelectedItems.Count == 1)
            {
                //ищем по тегу
                DealSet dealSet = listViewDealSet.SelectedItems[0].Tag as DealSet;
                //Обновляем данные
                dealSet.IdSupply = Convert.ToInt32(comboBoxSupply.SelectedItem.ToString().Split('.')[0]);
                dealSet.IdDemand = Convert.ToInt32(comboBoxDemand.SelectedItem.ToString().Split('.')[0]);
                //Сохраняем изменения
                Program.wftDB.SaveChanges();
                ShowDealSet();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                //Если в listView выбран элемент
                if (listViewDealSet.SelectedItems.Count == 1)
                {
                    //ищем по тегу
                    DealSet dealSet = listViewDealSet.SelectedItems[0].Tag as DealSet;
                    //Удаляем из базы
                    Program.wftDB.DealSet.Remove(dealSet);
                    //Сохраняем изменения
                    Program.wftDB.SaveChanges();
                    ShowDealSet();
                }
                //Очищаем поля
                comboBoxSupply.SelectedItem = null;
                comboBoxDemand.SelectedItem = null;
                ShowDealSet();
            }
            catch
            {
                MessageBox.Show("Невозможно удалить запись, возможно она используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewDealSet_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Если в listView выбран элемент
            if (listViewDealSet.SelectedItems.Count == 1)
            {
                //ищем по тегу
                DealSet dealSet = listViewDealSet.SelectedItems[0].Tag as DealSet;
                comboBoxSupply.SelectedIndex = comboBoxSupply.FindString(dealSet.IdSupply.ToString());
                comboBoxDemand.SelectedIndex = comboBoxDemand.FindString(dealSet.IdDemand.ToString());
            }
            //Если нет
            else
            {
                comboBoxSupply.SelectedItem = null;
                comboBoxDemand.SelectedItem = null;
            }
        }
    }
}
