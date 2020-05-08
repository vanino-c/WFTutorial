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
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
            ShowClients();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Новый экземпляр класса Клиент
            ClientSet clientsSet = new ClientSet();
            //Заполняем его данными
            clientsSet.FirstName = textBoxFirstName.Text;
            clientsSet.MiddleName = textBoxMiddleName.Text;
            clientsSet.LastName = textBoxLastName.Text;
            clientsSet.Phone = textBoxPhone.Text;
            clientsSet.Email = textBoxEmail.Text;
            //Добавляем в таблицу ClientSet нового клиента clientsSet
            Program.wftDB.ClientSet.Add(clientsSet);
            //Сохраняем изменения
            Program.wftDB.SaveChanges();
            //Обновляем listView
            ShowClients();
        }
        void ShowClients()
        {
            //Очищаем listView
            listViewClient.Items.Clear();
            //Проходим по коллекции клиентов в базе с помощью foreach
            foreach (ClientSet clientsSet in Program.wftDB.ClientSet)
            {
                //Создаём новый элемент для listView из нового массива строк
                ListViewItem item = new ListViewItem(new string[]
                {
                    //Добавляем данные
                    clientsSet.Id.ToString(),
                    clientsSet.LastName,
                    clientsSet.FirstName,
                    clientsSet.MiddleName,
                    clientsSet.Phone,
                    clientsSet.Email
                });
                //Указывваем по какому тегу будем брать элементы
                item.Tag = clientsSet;
                //Добавляем клиента в listView
                listViewClient.Items.Add(item);
            }
            //Выравниваем колонки в listView
            listViewClient.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {

            //ищем элемент из таблицы по тегу
            ClientSet clientsSet = listViewClient.SelectedItems[0].Tag as ClientSet;
            //Обновляем его данные
            clientsSet.FirstName = textBoxFirstName.Text;
            clientsSet.MiddleName = textBoxMiddleName.Text;
            clientsSet.LastName = textBoxLastName.Text;
            clientsSet.Phone = textBoxPhone.Text;
            clientsSet.Email = textBoxEmail.Text;
            //Сохраняем изменения
            Program.wftDB.SaveChanges();
            //Обновляем listView
            ShowClients();
        }

        private void listViewClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Если был выбран один элемент
            if (listViewClient.SelectedItems.Count == 1)
            {
                //ищем элемент из таблицы по тегу
                ClientSet clientsSet = listViewClient.SelectedItems[0].Tag as ClientSet;
                //Указываем, что может быть изменено
                textBoxFirstName.Text = clientsSet.FirstName;
                textBoxMiddleName.Text = clientsSet.MiddleName;
                textBoxLastName.Text = clientsSet.LastName;
                textBoxPhone.Text = clientsSet.Phone;
                textBoxEmail.Text = clientsSet.Email;
            }
            else
            {
                //Иначе очищаем поля для ввода
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                textBoxEmail.Text = "";
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
                if (listViewClient.SelectedItems.Count == 1)
                {
                    //ищем элемент из таблицы по тегу
                    ClientSet clientsSet = listViewClient.SelectedItems[0].Tag as ClientSet;
                    //И удаляем его из модели и базы данных
                    Program.wftDB.ClientSet.Remove(clientsSet);
                    //Сохраняем изменения
                    Program.wftDB.SaveChanges();
                    //Обновляем listView
                    ShowClients();
                }
                //Очищаем поля для ввода
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                textBoxEmail.Text = "";
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

            if (listViewClient.SelectedItems.Count == 1)
            {
                //ищем элемент из таблицы по тегу
                ClientSet clientsSet = listViewClient.SelectedItems[0].Tag as ClientSet;
                foreach (SupplySet supplySet in Program.wftDB.SupplySet)
                {
                    //Если нашли, добавляем в listBox
                    if (supplySet.IdAgent == clientsSet.Id)
                    {
                        string[] item =
                        {
                            "ID предложения: " + supplySet.Id.ToString() + ", ",
                            supplySet.AgentsSet.LastName + " " + supplySet.AgentsSet.FirstName.Substring(0, 1) + "." + supplySet.AgentsSet.MiddleName.Substring(0, 1) + "."
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

            if (listViewClient.SelectedItems.Count == 1)
            {
                //ищем элемент из таблицы по тегу
                ClientSet clientsSet = listViewClient.SelectedItems[0].Tag as ClientSet;
                foreach (DemandSet demandSet in Program.wftDB.DemandSet)
                {
                    //Если нашли, добавляем в listBox
                    if (demandSet.IdAgent == clientsSet.Id)
                    {
                        string[] item =
                        {
                            "ID потребности: " + demandSet.Id.ToString() + ", ",
                            demandSet.AgentsSet.LastName + " " + demandSet.AgentsSet.FirstName.Substring(0, 1) + "." + demandSet.AgentsSet.MiddleName.Substring(0, 1) + "."
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
