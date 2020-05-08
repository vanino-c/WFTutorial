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
    public partial class FormAgent : Form
    {
        public FormAgent()
        {
            InitializeComponent();
            ShowClients();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Если поля ФИО не пусты
            if (textBoxFirstName.Text != "" && textBoxMiddleName.Text != "" && textBoxLastName.Text != "")
            {
                //Новый экземпляр класса Риелтор
                AgentsSet agentsSet = new AgentsSet();
                //Заполняем его данными
                agentsSet.FirstName = textBoxFirstName.Text;
                agentsSet.MiddleName = textBoxMiddleName.Text;
                agentsSet.LastName = textBoxLastName.Text;
                agentsSet.Share = Convert.ToInt32(numericUpDownShare.Value);
                //Добавляем в таблицу AgentsSet нового риэлтора agentsSet
                Program.wftDB.AgentsSet.Add(agentsSet);
                //Сохраняем изменения
                Program.wftDB.SaveChanges();
                //Обновляем listView
                ShowClients();
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ShowClients()
        {
            //Очищаем listView
            listViewAgent.Items.Clear();
            //Проходим по коллекции клиентов в базе с помощью foreach
            foreach (AgentsSet agentsSet in Program.wftDB.AgentsSet)
            {
                //Создаём новый элемент для listView из нового массива строк
                ListViewItem item = new ListViewItem(new string[]
                {
                    //Добавляем данные
                    agentsSet.Id.ToString(),
                    agentsSet.LastName,
                    agentsSet.FirstName,
                    agentsSet.MiddleName,
                    agentsSet.Share.ToString()
                });
                //Указываем по какому тегу будем брать элементы
                item.Tag = agentsSet;
                //Добавляем клиента в listView
                listViewAgent.Items.Add(item);
            }
            //Выравниваем колонки в listView
            listViewAgent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {

            //ищем элемент из таблицы по тегу
            AgentsSet agentsSet = listViewAgent.SelectedItems[0].Tag as AgentsSet;
            //Обновляем его данные
            agentsSet.FirstName = textBoxFirstName.Text;
            agentsSet.MiddleName = textBoxMiddleName.Text;
            agentsSet.LastName = textBoxLastName.Text;
            agentsSet.Share = Convert.ToInt32(numericUpDownShare.Value);
            //Сохраняем изменения
            Program.wftDB.SaveChanges();
            //Обновляем listView
            ShowClients();
        }

        private void listViewClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Если был выбран один элемент
            if (listViewAgent.SelectedItems.Count == 1)
            {
                //ищем элемент из таблицы по тегу
                AgentsSet agentsSet = listViewAgent.SelectedItems[0].Tag as AgentsSet;
                //Указываем, что может быть изменено
                textBoxFirstName.Text = agentsSet.FirstName;
                textBoxMiddleName.Text = agentsSet.MiddleName;
                textBoxLastName.Text = agentsSet.LastName;
                numericUpDownShare.Value = agentsSet.Share;
            }
            else
            {
                //Иначе очищаем поля для ввода
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                numericUpDownShare.Value = 0;
            }
            ShowSupply();
            ShowDemands();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            //Пробуем удалить клиента
            try
            {
                //Если выбран один элемент из listView
                if (listViewAgent.SelectedItems.Count == 1)
                {
                    //ищем элемент из таблицы по тегу
                    AgentsSet agentsSet = listViewAgent.SelectedItems[0].Tag as AgentsSet;
                    //Если риелтер не связан с потребностью или предложением 
                    if (listBoxDemand.Items.Count == 0 && listBoxSupply.Items.Count == 0)
                    {
                        //Удаляем его из модели и базы данных
                        Program.wftDB.AgentsSet.Remove(agentsSet);
                        //Сохраняем изменения
                        Program.wftDB.SaveChanges();
                        //Обновляем listView
                        ShowClients();
                    }
                    else
                    {
                        MessageBox.Show("Невозможно удалить запись, риелтер имеет связи с потребностью или предложением!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                //Очищаем поля для ввода
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                numericUpDownShare.Value = 0;
            }
            //Если удалить не получилось, например если запись используется, выводим сообщение
            catch
            {
                //Всплывающее окно, с параметрами Текст, Заголовок, Кнопка ОК и иконка ошибки
                MessageBox.Show("Невозможно удалить запись, возможно она используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ShowSupply()
        {

            if (listViewAgent.SelectedItems.Count == 1)
            {
                //ищем элемент из таблицы по тегу
                AgentsSet agentsSet = listViewAgent.SelectedItems[0].Tag as AgentsSet;
                foreach (SupplySet supplySet in Program.wftDB.SupplySet)
                {
                    //Если нашли, добавляем в listBox
                    if (supplySet.IdAgent == agentsSet.Id)
                    {
                        string[] item =
                        {
                            "ID предложения: " + supplySet.Id.ToString() + ", ",
                            supplySet.ClientSet.LastName + " " + supplySet.ClientSet.FirstName.Substring(0, 1) + "." + supplySet.ClientSet.MiddleName.Substring(0, 1) + "."
                        };
                        listBoxSupply.Items.Add(string.Join(" ", item));
                    }
                }
            }
            else
            {
                //Очищаем listBox
                listBoxSupply.Items.Clear();
            }
        }

        void ShowDemands()
        {

            if (listViewAgent.SelectedItems.Count == 1)
            {
                //ищем элемент из таблицы по тегу
                AgentsSet agentsSet = listViewAgent.SelectedItems[0].Tag as AgentsSet;
                foreach (DemandSet demandSet in Program.wftDB.DemandSet)
                {
                    //Если нашли, добавляем в listBox
                    if (demandSet.IdAgent == agentsSet.Id)
                    {
                        string[] item =
                        {
                            "ID потребности: " + demandSet.Id.ToString() + ", ",
                            demandSet.ClientSet.LastName + " " + demandSet.ClientSet.FirstName.Substring(0, 1) + "." + demandSet.ClientSet.MiddleName.Substring(0, 1) + "."
                        };
                        listBoxDemand.Items.Add(string.Join(" ", item));
                    }
                }
            }
            else
            {
                //Очищаем listBox
                listBoxDemand.Items.Clear();
            }
        }
    }
}
