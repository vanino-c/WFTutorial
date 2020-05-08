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
    public partial class FormDemands : Form
    {
        public FormDemands()
        {
            InitializeComponent();
            comboBoxType.SelectedIndex = 0;
            ShowAgents();
            ShowClients();
            ShowDemandsSet();
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

        void ShowDemandsSet()
        {
            //Очищаем listView
            listViewRealEstateSet_Apartment.Items.Clear();
            listViewRealEstateSet_House.Items.Clear();
            listViewRealEstateSet_Land.Items.Clear();
            //Проходим по коллекции недвижимости в базе с помощью foreach
            foreach (DemandSet demand in Program.wftDB.DemandSet)
            {
                //Квартира
                if (demand.Type == 0)
                {
                    //Создаём новый элемент для listViewRealEstateSet_Apartment из нового массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        demand.AgentsSet.LastName + " " + demand.AgentsSet.FirstName + " " + demand.AgentsSet.MiddleName,
                        demand.ClientSet.LastName + " " + demand.ClientSet.FirstName + " " + demand.ClientSet.MiddleName,
                        "Квартира",
                        demand.MinPrice.ToString(),
                        demand.MaxPrice.ToString(),
                        demand.MinArea.ToString(),
                        demand.MaxArea.ToString(),
                        demand.MinFloor.ToString(),
                        demand.MaxFloor.ToString(),
                        demand.MinRooms.ToString(),
                        demand.MaxRooms.ToString()
                    });
                    //Указываем по какому тегу будем брать элементы
                    item.Tag = demand;
                    //Добавляем клиента в listViewRealEstateSet_Apartment
                    listViewRealEstateSet_Apartment.Items.Add(item);
                }
                //Дом
                else if (demand.Type == 1)
                {
                    //Создаём новый элемент для listViewRealEstateSet_House из нового массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        demand.AgentsSet.LastName + " " + demand.AgentsSet.FirstName + " " + demand.AgentsSet.MiddleName,
                        demand.ClientSet.LastName + " " + demand.ClientSet.FirstName + " " + demand.ClientSet.MiddleName,
                        "Квартира",
                        demand.MinPrice.ToString(),
                        demand.MaxPrice.ToString(),
                        demand.MinArea.ToString(),
                        demand.MaxArea.ToString(),
                        demand.MinFloors.ToString(),
                        demand.MaxFloors.ToString(),
                    });
                    //Указываем по какому тегу будем брать элементы
                    item.Tag = demand;
                    //Добавляем клиента в listViewRealEstateSet_House
                    listViewRealEstateSet_House.Items.Add(item);
                }
                //Земля
                else if (demand.Type == 2)
                {
                    //Создаём новый элемент для listViewRealEstateSet_Land из нового массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        demand.AgentsSet.LastName + " " + demand.AgentsSet.FirstName + " " + demand.AgentsSet.MiddleName,
                        demand.ClientSet.LastName + " " + demand.ClientSet.FirstName + " " + demand.ClientSet.MiddleName,
                        "Квартира",
                        demand.MinPrice.ToString(),
                        demand.MaxPrice.ToString(),
                        demand.MinArea.ToString(),
                        demand.MaxArea.ToString()
                    });
                    //Указываем по какому тегу будем брать элементы
                    item.Tag = demand;
                    //Добавляем клиента в listViewRealEstateSet_Land
                    listViewRealEstateSet_Land.Items.Add(item);
                }
            }
            //Выравниваем столбцы по ширине заголовка
            listViewRealEstateSet_Land.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRealEstateSet_House.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRealEstateSet_Apartment.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Если выбрана квартира
            if (comboBoxType.SelectedIndex == 0)
            {
                //Показываем нужные элементы
                listViewRealEstateSet_Apartment.Visible = true;
                labelMaxFloor.Visible = true;
                labelMinFloor.Visible = true;
                textBoxMaxFloor.Visible = true;
                textBoxMinFloor.Visible = true;
                labelMaxRooms.Visible = true;
                labelMinRooms.Visible = true;
                textBoxMaxRooms.Visible = true;
                textBoxMinRooms.Visible = true;

                //Скрываем ненужные
                listViewRealEstateSet_House.Visible = false;
                listViewRealEstateSet_Land.Visible = false;
                labelMaxFloors.Visible = false;
                labelMinFloors.Visible = false;
                textBoxMaxFloors.Visible = false;
                textBoxMinFloors.Visible = false;

                //Очищаем видимые элементы
                textBoxMaxPrice.Text = "";
                textBoxMinPrice.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMinArea.Text = "";
                textBoxMaxRooms.Text = "";
                textBoxMinRooms.Text = "";
                textBoxMaxFloor.Text = "";
                textBoxMinFloor.Text = "";
            }
            //Если выбран дом
            else if (comboBoxType.SelectedIndex == 1)
            {
                //Показываем нужные элементы
                listViewRealEstateSet_House.Visible = true;
                labelMaxFloors.Visible = true;
                labelMinFloors.Visible = true;
                textBoxMinFloors.Visible = true;
                textBoxMaxFloors.Visible = true;

                //Скрываем ненужные
                listViewRealEstateSet_Apartment.Visible = false;
                listViewRealEstateSet_Land.Visible = false;
                labelMaxFloor.Visible = false;
                labelMinFloor.Visible = false;
                textBoxMaxFloor.Visible = false;
                textBoxMinFloor.Visible = false;
                labelMaxRooms.Visible = false;
                labelMinRooms.Visible = false;
                textBoxMaxRooms.Visible = false;
                textBoxMinRooms.Visible = false;

                //Очищаем видимые элементы
                textBoxMaxPrice.Text = "";
                textBoxMinPrice.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMinArea.Text = "";
                textBoxMaxFloors.Text = "";
                textBoxMinFloors.Text = "";
            }
            //Если выбрана земля
            else if (comboBoxType.SelectedIndex == 2)
            {
                //Показываем нужные элементы
                listViewRealEstateSet_Land.Visible = true;

                //Скрываем ненужные
                listViewRealEstateSet_House.Visible = false;
                listViewRealEstateSet_Apartment.Visible = false;
                labelMaxFloors.Visible = false;
                labelMinFloors.Visible = false;
                textBoxMaxFloors.Visible = false;
                textBoxMinFloors.Visible = false;
                labelMaxFloor.Visible = false;
                labelMinFloor.Visible = false;
                textBoxMaxFloor.Visible = false;
                textBoxMinFloor.Visible = false;
                labelMaxRooms.Visible = false;
                labelMinRooms.Visible = false;
                textBoxMaxRooms.Visible = false;
                textBoxMinRooms.Visible = false;

                //Очищаем видимые элементы
                textBoxMaxPrice.Text = "";
                textBoxMinPrice.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMinArea.Text = "";
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxAgents.SelectedItem != null && comboBoxClients.SelectedItem != null
                   && comboBoxType.SelectedItem != null)
            {
                //Новый экземпляр класса Потребности
                DemandSet demand = new DemandSet();
                //Из выбранной строки в comboBoxAgents отделяем ID риелтора(после него точка) и делаем ссылку demand.IdAgent
                demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                //Так же отделяем ID клиента
                demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                //Деньги лучше держать в Int64
                demand.MinPrice = Convert.ToInt64(textBoxMinPrice.Text);
                demand.MaxPrice = Convert.ToInt64(textBoxMaxPrice.Text);
                demand.MinArea = Convert.ToDouble(textBoxMinArea.Text);
                demand.MaxArea = Convert.ToDouble(textBoxMaxArea.Text);
                //Дополнительные поля типа Квартира
                if (comboBoxType.SelectedIndex == 0)
                {
                    demand.Type = 0;
                    demand.MinFloor = Convert.ToInt32(textBoxMinFloor.Text);
                    demand.MaxFloor = Convert.ToInt32(textBoxMaxFloor.Text);
                    demand.MinRooms = Convert.ToInt32(textBoxMinRooms.Text);
                    demand.MaxRooms = Convert.ToInt32(textBoxMaxRooms.Text);
                }
                //Дополнительные поля типа Дом
                else if (comboBoxType.SelectedIndex == 1)
                {
                    demand.Type = 1;
                    demand.MinFloors = Convert.ToInt32(textBoxMinFloors.Text);
                    demand.MaxFloors = Convert.ToInt32(textBoxMaxFloors.Text);
                }
                //Дополнительные поля типа Земля
                else if (comboBoxType.SelectedIndex == 2)
                {
                    demand.Type = 2;
                }
                //Добавляем новый объект недвижимости demand в таблицу DemandSet
                Program.wftDB.DemandSet.Add(demand);
                //Сохраняем изменения
                Program.wftDB.SaveChanges();
                ShowDemandsSet();
            }
            else MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //Если выбрана Квартира
            if (comboBoxType.SelectedIndex == 0)
            {
                //И выбран элемент списка
                if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1)
                {
                    //Ищем элемент из таблицы по тегу
                    DemandSet demand = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as DemandSet;
                    //Обновляем данные
                    demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                    demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                    demand.MinPrice = Convert.ToInt64(textBoxMinPrice.Text);
                    demand.MaxPrice = Convert.ToInt64(textBoxMaxPrice.Text);
                    demand.MinArea = Convert.ToDouble(textBoxMinArea.Text);
                    demand.MaxArea = Convert.ToDouble(textBoxMaxArea.Text);
                    demand.MinFloor = Convert.ToInt32(textBoxMinFloor.Text);
                    demand.MaxFloor = Convert.ToInt32(textBoxMaxFloor.Text);
                    demand.MinRooms = Convert.ToInt32(textBoxMinRooms.Text);
                    demand.MaxRooms = Convert.ToInt32(textBoxMaxRooms.Text);
                    //Сохраняем изминения в модели wftDB
                    Program.wftDB.SaveChanges();
                    //Обновляем списки
                    ShowDemandsSet();
                }
            }
            //Если выбран Дом
            else if (comboBoxType.SelectedIndex == 1)
            {
                //И выбран элемент списка
                if (listViewRealEstateSet_House.SelectedItems.Count == 1)
                {
                    //Ищем элемент из таблицы по тегу
                    DemandSet demand = listViewRealEstateSet_House.SelectedItems[0].Tag as DemandSet;
                    //Обновляем данные
                    demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                    demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                    demand.MinPrice = Convert.ToInt64(textBoxMinPrice.Text);
                    demand.MaxPrice = Convert.ToInt64(textBoxMaxPrice.Text);
                    demand.MinArea = Convert.ToDouble(textBoxMinArea.Text);
                    demand.MaxArea = Convert.ToDouble(textBoxMaxArea.Text);
                    demand.MinFloors = Convert.ToInt32(textBoxMinFloors.Text);
                    demand.MaxFloors = Convert.ToInt32(textBoxMaxFloors.Text);
                    //Сохраняем изминения в модели wftDB
                    Program.wftDB.SaveChanges();
                    //Обновляем списки
                    ShowDemandsSet();
                }
            }
            //Если выбрана Земля
            else if (comboBoxType.SelectedIndex == 2)
            {
                //И выбран элемент списка
                if (listViewRealEstateSet_Land.SelectedItems.Count == 1)
                {
                    //Ищем элемент из таблицы по тегу
                    DemandSet demand = listViewRealEstateSet_Land.SelectedItems[0].Tag as DemandSet;
                    //Обновляем данные
                    demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                    demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                    demand.MinPrice = Convert.ToInt64(textBoxMinPrice.Text);
                    demand.MaxPrice = Convert.ToInt64(textBoxMaxPrice.Text);
                    demand.MinArea = Convert.ToDouble(textBoxMinArea.Text);
                    demand.MaxArea = Convert.ToDouble(textBoxMaxArea.Text);
                    //Сохраняем изминения в модели wftDB
                    Program.wftDB.SaveChanges();
                    //Обновляем списки
                    ShowDemandsSet();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            //Пробуем удалить
            try
            {
                //Если выбрана Квартира
                if (comboBoxType.SelectedIndex == 0)
                {
                    //И выбран элемент списка
                    if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1)
                    {
                        //Ищем элемент из таблицы по тегу
                        DemandSet demand = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as DemandSet;
                        //И удаляем его из базы данных
                        Program.wftDB.DemandSet.Remove(demand);
                        //Сохраняем изменения
                        Program.wftDB.SaveChanges();
                        //Обновляем список
                        ShowDemandsSet();
                    }
                    //Очищаем поля формы
                    textBoxMaxPrice.Text = "";
                    textBoxMinPrice.Text = "";
                    textBoxMaxArea.Text = "";
                    textBoxMinArea.Text = "";
                    textBoxMaxRooms.Text = "";
                    textBoxMinRooms.Text = "";
                    textBoxMaxFloor.Text = "";
                    textBoxMinFloor.Text = "";
                }
                //Если выбран Дом
                if (comboBoxType.SelectedIndex == 1)
                {
                    //И выбран элемент списка
                    if (listViewRealEstateSet_House.SelectedItems.Count == 1)
                    {
                        //Ищем элемент из таблицы по тегу
                        DemandSet demand = listViewRealEstateSet_House.SelectedItems[0].Tag as DemandSet;
                        //И удаляем его из базы данных
                        Program.wftDB.DemandSet.Remove(demand);
                        //Сохраняем изменения
                        Program.wftDB.SaveChanges();
                        //Обновляем список
                        ShowDemandsSet();
                    }
                    //Очищаем поля формы
                    textBoxMaxPrice.Text = "";
                    textBoxMinPrice.Text = "";
                    textBoxMaxArea.Text = "";
                    textBoxMinArea.Text = "";
                    textBoxMaxFloors.Text = "";
                    textBoxMinFloors.Text = "";
                }
                //Если выбрана Земля
                else if (comboBoxType.SelectedIndex == 2)
                {
                    //И выбран элемент списка
                    if (listViewRealEstateSet_Land.SelectedItems.Count == 1)
                    {
                        //Ищем элемент из таблицы по тегу
                        DemandSet demand = listViewRealEstateSet_Land.SelectedItems[0].Tag as DemandSet;
                        //И удаляем его из базы данных
                        Program.wftDB.DemandSet.Remove(demand);
                        //Сохраняем изменения
                        Program.wftDB.SaveChanges();
                        //Обновляем список
                        ShowDemandsSet();
                    }
                    //Очищаем поля формы
                    textBoxMaxPrice.Text = "";
                    textBoxMinPrice.Text = "";
                    textBoxMaxArea.Text = "";
                    textBoxMinArea.Text = "";
                }
            }
            //Если что-то пошло не так
            catch
            {
                MessageBox.Show("Невозможно удалить запись, возможно она используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewRealEstateSet_Land_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRealEstateSet_Land.SelectedItems.Count == 1)
            {
                //Ищем элемент из таблицы по тегу
                DemandSet demand = listViewRealEstateSet_Land.SelectedItems[0].Tag as DemandSet;
                //Обновляем данные на форме
                //Ищем в comboBoxAgents строку по ID риелтора и отображаем её
                comboBoxAgents.SelectedIndex = comboBoxAgents.FindString(demand.IdAgent.ToString());
                //Тоже для comboBoxClients
                comboBoxClients.SelectedIndex = comboBoxClients.FindString(demand.IdClient.ToString());
                textBoxMaxPrice.Text = demand.MaxPrice.ToString();
                textBoxMinPrice.Text = demand.MinPrice.ToString();
                textBoxMaxArea.Text = demand.MaxArea.ToString();
                textBoxMinArea.Text = demand.MinArea.ToString();
            }
            else
            {
                //Если ничего не выбрано, очищаем поля
                textBoxMaxPrice.Text = "";
                textBoxMinPrice.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMinArea.Text = "";
            }
        }

        private void listViewRealEstateSet_House_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1)
            {
                //Ищем элемент из таблицы по тегу
                DemandSet demand = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as DemandSet;
                //Обновляем данные на форме
                //Ищем в comboBoxAgents строку по ID риелтора и отображаем её
                comboBoxAgents.SelectedIndex = comboBoxAgents.FindString(demand.IdAgent.ToString());
                //Тоже для comboBoxClients
                comboBoxClients.SelectedIndex = comboBoxClients.FindString(demand.IdClient.ToString());
                textBoxMaxPrice.Text = demand.MaxPrice.ToString();
                textBoxMinPrice.Text = demand.MinPrice.ToString();
                textBoxMaxArea.Text = demand.MaxArea.ToString();
                textBoxMinArea.Text = demand.MinArea.ToString();
                textBoxMaxFloors.Text = demand.MaxFloors.ToString();
                textBoxMinFloors.Text = demand.MinFloors.ToString();
            }
            else
            {
                //Если ничего не выбрано, очищаем поля
                textBoxMaxPrice.Text = "";
                textBoxMinPrice.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMinArea.Text = "";
                textBoxMaxFloors.Text = "";
                textBoxMinFloors.Text = "";
            }
        }

        private void listViewRealEstateSet_Apartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1)
            {
                //Ищем элемент из таблицы по тегу
                DemandSet demand = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as DemandSet;
                //Обновляем данные на форме
                //Ищем в comboBoxAgents строку по ID риелтора и отображаем её
                comboBoxAgents.SelectedIndex = comboBoxAgents.FindString(demand.IdAgent.ToString());
                //Тоже для comboBoxClients
                comboBoxClients.SelectedIndex = comboBoxClients.FindString(demand.IdClient.ToString());
                textBoxMaxPrice.Text = demand.MaxPrice.ToString();
                textBoxMinPrice.Text = demand.MinPrice.ToString();
                textBoxMaxArea.Text = demand.MaxArea.ToString();
                textBoxMinArea.Text = demand.MinArea.ToString();
                textBoxMaxRooms.Text = demand.MaxRooms.ToString();
                textBoxMinRooms.Text = demand.MinRooms.ToString();
                textBoxMaxFloor.Text = demand.MaxFloor.ToString();
                textBoxMinFloor.Text = demand.MinFloor.ToString();
            }
            else
            {
                //Если ничего не выбрано, очищаем поля
                textBoxMaxPrice.Text = "";
                textBoxMinPrice.Text = "";
                textBoxMaxArea.Text = "";
                textBoxMinArea.Text = "";
                textBoxMaxRooms.Text = "";
                textBoxMinRooms.Text = "";
                textBoxMaxFloor.Text = "";
                textBoxMinFloor.Text = "";
            }
        }
    }
}
