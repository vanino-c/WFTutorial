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
    public partial class FormSupply : Form
    {
        public FormSupply()
        {
            InitializeComponent();
            ShowAgents();
            ShowClients();
            ShowRealEstates();
            ShowSupplySet();
        }

        void ShowAgents()
        {
            //Очищаем comboBox
            comboBoxAgents.Items.Clear();
            foreach (AgentsSet agentsSet in Program.wftDB.AgentsSet)
            {
                //Добавляем Риелтора с нужной нам информацией(ID, Фамилия, инициалы, доля)
                string[] item =
                {
                    agentsSet.Id.ToString() + ".",
                    agentsSet.LastName,
                    agentsSet.FirstName.Substring(0, 1) + "." + agentsSet.MiddleName.Substring(0, 1) + ".",
                    "Комиссия: " + agentsSet.Share.ToString()
                };
                comboBoxAgents.Items.Add(string.Join(" ", item));
            }
        }
        void ShowClients()
        {
            //Очищаем comboBox
            comboBoxClients.Items.Clear();
            foreach (ClientSet clientsSet in Program.wftDB.ClientSet)
            {
                //Добавляем Клиента с нужной нам информацией(ID, Фамилия, инициалы)
                string[] item =
                {
                    clientsSet.Id.ToString() + ".",
                    clientsSet.LastName,
                    clientsSet.FirstName.Substring(0, 1) + "." + clientsSet.MiddleName.Substring(0, 1) + "."
                };
                comboBoxClients.Items.Add(string.Join(" ", item));
            }
        }
        void ShowRealEstates()
        {
            //Очищаем comboBox
            comboBoxRealEstates.Items.Clear();
            foreach (RealEstateSet realEstate in Program.wftDB.RealEstateSet)
            {
                //Добавляем Клиента с нужной нам информацией(ID, Город, Улица, Дом, Квартира, Площадь)
                string[] item =
                {
                    realEstate.Id.ToString() + ".",
                    realEstate.Address_City + ",",
                    realEstate.Address_Street + ",",
                    "д." + realEstate.Address_House,
                    "кв." + realEstate.Address_Number,
                    "площадь " + realEstate.TotalArea,
                };
                comboBoxRealEstates.Items.Add(string.Join(" ", item));
            }
        }

        void ShowSupplySet()
        {
            //Очищаем listView
            listViewSupplySet.Items.Clear();
            foreach (SupplySet supply in Program.wftDB.SupplySet)
            {
                //Новый элемент из массива строк
                ListViewItem item = new ListViewItem(new string[]
                {
                    supply.AgentsSet.LastName + " " + supply.AgentsSet.FirstName + " " + supply.AgentsSet.MiddleName,
                    supply.ClientSet.LastName + " " + supply.ClientSet.FirstName + " " + supply.ClientSet.MiddleName,
                    supply.RealEstateSet.Id.ToString() + ". г. " + supply.RealEstateSet.Address_City + ", ул. " +
                    supply.RealEstateSet.Address_Street + ", д. " + supply.RealEstateSet.Address_House +
                    " кв. " + supply.RealEstateSet.Address_Number + " площадь " + supply.RealEstateSet.TotalArea,
                    supply.Price.ToString()
                });
                //Тег элементов
                item.Tag = supply;
                //Добавляем в listViewSupplySet
                listViewSupplySet.Items.Add(item);
            }

            //Выравниваем колонки в listView
            listViewSupplySet.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxAgents.SelectedItem != null && comboBoxClients.SelectedItem != null
                && comboBoxRealEstates.SelectedItem != null && textBoxPrice.Text != "")
            {
                //Новый экземпляр класса Предложение
                SupplySet supply = new SupplySet();
                //Из выбранной строки в comboBoxAgents отделяем ID риелтора(после него точка) и делаем ссылку supply.IdAgent
                supply.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                //Так же отделяем ID клиента
                supply.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                //И ID недвижимости
                supply.IdRealEstate = Convert.ToInt32(comboBoxRealEstates.SelectedItem.ToString().Split('.')[0]);
                //Цена на недвижимость обычно большая, лучше хранить в Int64
                supply.Price = Convert.ToInt64(textBoxPrice.Text);
                //Добавляем новое предложение supply в таблицу SupplySet
                Program.wftDB.SupplySet.Add(supply);
                //Сохраняем изменения
                Program.wftDB.SaveChanges();
                ShowSupplySet();
            }
            else MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewSupplySet.SelectedItems.Count == 1)
            {
                //Ищем элемент по тегу
                SupplySet supply = listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                //Обновляем данные
                supply.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                supply.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                supply.IdRealEstate = Convert.ToInt32(comboBoxRealEstates.SelectedItem.ToString().Split('.')[0]);
                supply.Price = Convert.ToInt64(textBoxPrice.Text);
                //Сохраняем изменения
                Program.wftDB.SaveChanges();
                ShowSupplySet();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            //Пробуем удалить
            try
            {
                if (listViewSupplySet.SelectedItems.Count == 1)
                {
                    //Ищем элемент по тегу
                    SupplySet supply = listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                    //Удаляем его из базы данных
                    Program.wftDB.SupplySet.Remove(supply);
                    //Сохраняем изменения
                    Program.wftDB.SaveChanges();
                    //Обновляем список
                    ShowSupplySet();
                }
            }
            catch
            {
                MessageBox.Show("Невозможно удалить запись, возможно она используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewSupplySet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSupplySet.SelectedItems.Count == 1)
            {
                //Ищем элемент по тегу
                SupplySet supply = listViewSupplySet.SelectedItems[0].Tag as SupplySet;
                //Ищем в comboBoxAgents строку по ID риелтора и отображаем её
                comboBoxAgents.SelectedIndex = comboBoxAgents.FindString(supply.IdAgent.ToString());
                //Тоже для comboBoxClients и comboBoxRealEstates
                comboBoxClients.SelectedIndex = comboBoxClients.FindString(supply.IdClient.ToString());
                comboBoxRealEstates.SelectedIndex = comboBoxRealEstates.FindString(supply.IdRealEstate.ToString());
                textBoxPrice.Text = supply.Price.ToString();
            }
            else
            {
                comboBoxAgents.SelectedItem = null;
                comboBoxClients.SelectedItem = null;
                comboBoxRealEstates.SelectedItem = null;
                textBoxPrice.Text = "";
            }
        }
    }
}
