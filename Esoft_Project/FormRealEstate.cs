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
    public partial class FormRealEstate : Form
    {
        public FormRealEstate()
        {
            InitializeComponent();
            comboBoxType.SelectedIndex = 0;
            ShowRealEstateSet();
        }

        private void istViewRealEstateSet_Apartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1)
            {
                //Ищем элемент из таблицы по тегу
                RealEstateSet realEstate = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as RealEstateSet;
                //Обновляем данные на форме
                textBoxAddress_City.Text = realEstate.Address_City;
                textBoxAddress_House.Text = realEstate.Address_House;
                textBoxAddress_Street.Text = realEstate.Address_Street;
                textBoxAddress_Number.Text = realEstate.Address_Number;
                textBoxCoordinate_latitude.Text = realEstate.Coordinate_Latitude.ToString();
                textBoxCoordinate_longitude.Text = realEstate.Coordinate_Longitude.ToString();
                textBoxTotalArea.Text = realEstate.TotalArea.ToString();
                textBoxRooms.Text = realEstate.Rooms.ToString();
                textBoxFloor.Text = realEstate.Floor.ToString();
            }
            else
            {
                //Если ничего не выбрано, очищаем поля
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxRooms.Text = "";
                textBoxFloor.Text = "";
            }
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Если выбрана квартира
            if (comboBoxType.SelectedIndex == 0)
            {
                //Показываем нужные элементы
                listViewRealEstateSet_Apartment.Visible = true;
                labelFloor.Visible = true;
                textBoxFloor.Visible = true;
                labelRooms.Visible = true;
                textBoxRooms.Visible = true;

                //Скрываем ненужные
                listViewRealEstateSet_House.Visible = false;
                listViewRealEstateSet_Land.Visible = false;
                labelTotalFloors.Visible = false;
                textBoxTotalFloors.Visible = false;

                //Очищаем видимые элементы
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxRooms.Text = "";
                textBoxFloor.Text = "";
            }
            //Если выбран дом
            else if (comboBoxType.SelectedIndex == 1)
            {
                //Показываем нужные элементы
                listViewRealEstateSet_House.Visible = true;
                labelTotalFloors.Visible = true;
                textBoxTotalFloors.Visible = true;

                //Скрываем ненужные
                listViewRealEstateSet_Apartment.Visible = false;
                listViewRealEstateSet_Land.Visible = false;
                labelFloor.Visible = false;
                textBoxFloor.Visible = false;
                labelRooms.Visible = false;
                textBoxRooms.Visible = false;

                //Очищаем видимые элементы
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxTotalFloors.Text = "";
            }
            //Если выбрана земля
            else if (comboBoxType.SelectedIndex == 2)
            {
                //Показываем нужные элементы
                listViewRealEstateSet_Land.Visible = true;

                //Скрываем ненужные
                listViewRealEstateSet_House.Visible = false;
                listViewRealEstateSet_Apartment.Visible = false;
                labelTotalFloors.Visible = false;
                textBoxTotalFloors.Visible = false;
                labelFloor.Visible = false;
                textBoxFloor.Visible = false;
                labelRooms.Visible = false;
                textBoxRooms.Visible = false;

                //Очищаем видимые элементы
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Широта и долгота
            double latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text);
            double longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text);
            //Они должны быть в пределах -90 до +90 и -180 до +180 соответственно
            if (((-90.0 <= latitude) && (latitude <= 90.0)) && ((-180.0 <= latitude) && (latitude <= 180.0)))
            { 
                //Новый экземпляр класса Объект недвижимости
                RealEstateSet realEstate = new RealEstateSet();
                //Заполняем его значениями из textbox-ов(общее)
                realEstate.Address_City = textBoxAddress_City.Text;
                realEstate.Address_House = textBoxAddress_House.Text;
                realEstate.Address_Street = textBoxAddress_Street.Text;
                realEstate.Address_Number = textBoxAddress_Number.Text;
                realEstate.Coordinate_Latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text);
                realEstate.Coordinate_Longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text);
                realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text);
                //Дополнительные поля типа Квартира
                if (comboBoxType.SelectedIndex == 0)
                {
                    realEstate.Type = 0;
                    realEstate.Rooms = Convert.ToInt32(textBoxRooms.Text);
                    realEstate.Floor = Convert.ToInt32(textBoxFloor.Text);
                }
                //Дополнительные поля типа Дом
                else if (comboBoxType.SelectedIndex == 1)
                {
                    realEstate.Type = 1;
                    realEstate.TotalFloors = Convert.ToInt32(textBoxTotalFloors.Text);
                }
                //Дополнительные поля типа Земля
                else if (comboBoxType.SelectedIndex == 2)
                {
                    realEstate.Type = 2;
                }
                //Добавляем новый объект недвижимости realEstate в таблицу RealEstateSet
                Program.wftDB.RealEstateSet.Add(realEstate);
                //Сохраняем изменения
                Program.wftDB.SaveChanges();
                ShowRealEstateSet();
            }
            else MessageBox.Show("Широта и долгота должны быть в пределах -90 до +90 и -180 до +180 соответственно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    RealEstateSet realEstate = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as RealEstateSet;
                    //Обновляем данные
                    realEstate.Address_City = textBoxAddress_City.Text;
                    realEstate.Address_House = textBoxAddress_House.Text;
                    realEstate.Address_Street = textBoxAddress_Street.Text;
                    realEstate.Address_Number = textBoxAddress_Number.Text;
                    realEstate.Coordinate_Latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text);
                    realEstate.Coordinate_Longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text);
                    realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text);
                    realEstate.Rooms = Convert.ToInt32(textBoxRooms.Text);
                    realEstate.Floor = Convert.ToInt32(textBoxFloor.Text);
                    //Сохраняем изминения в модели wftDB
                    Program.wftDB.SaveChanges();
                    //Обновляем списки
                    ShowRealEstateSet();
                }
            }
            //Если выбран Дом
            else if (comboBoxType.SelectedIndex == 1)
            {
                //И выбран элемент списка
                if (listViewRealEstateSet_House.SelectedItems.Count == 1)
                {
                    //Ищем элемент из таблицы по тегу
                    RealEstateSet realEstate = listViewRealEstateSet_House.SelectedItems[0].Tag as RealEstateSet;
                    //Обновляем данные
                    realEstate.Address_City = textBoxAddress_City.Text;
                    realEstate.Address_House = textBoxAddress_House.Text;
                    realEstate.Address_Street = textBoxAddress_Street.Text;
                    realEstate.Address_Number = textBoxAddress_Number.Text;
                    realEstate.Coordinate_Latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text);
                    realEstate.Coordinate_Longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text);
                    realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text);
                    realEstate.TotalFloors = Convert.ToInt32(textBoxTotalFloors.Text);
                    //Сохраняем изминения в модели wftDB
                    Program.wftDB.SaveChanges();
                    //Обновляем списки
                    ShowRealEstateSet();
                }
            }
            //Если выбрана Земля
            else if (comboBoxType.SelectedIndex == 2)
            {
                //И выбран элемент списка
                if (listViewRealEstateSet_Land.SelectedItems.Count == 1)
                {
                    //Ищем элемент из таблицы по тегу
                    RealEstateSet realEstate = listViewRealEstateSet_Land.SelectedItems[0].Tag as RealEstateSet;
                    //Обновляем данные
                    realEstate.Address_City = textBoxAddress_City.Text;
                    realEstate.Address_House = textBoxAddress_House.Text;
                    realEstate.Address_Street = textBoxAddress_Street.Text;
                    realEstate.Address_Number = textBoxAddress_Number.Text;
                    realEstate.Coordinate_Latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text);
                    realEstate.Coordinate_Longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text);
                    realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text);
                    //Сохраняем изминения в модели wftDB
                    Program.wftDB.SaveChanges();
                    //Обновляем списки
                    ShowRealEstateSet();
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
                        RealEstateSet realEstate = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as RealEstateSet;
                        //И удаляем его из базы данных
                        Program.wftDB.RealEstateSet.Remove(realEstate);
                        //Сохраняем изменения
                        Program.wftDB.SaveChanges();
                        //Обновляем список
                        ShowRealEstateSet();
                    }
                    //Очищаем поля формы
                    textBoxAddress_City.Text = "";
                    textBoxAddress_House.Text = "";
                    textBoxAddress_Street.Text = "";
                    textBoxAddress_Number.Text = "";
                    textBoxCoordinate_latitude.Text = "";
                    textBoxCoordinate_longitude.Text = "";
                    textBoxTotalArea.Text = "";
                    textBoxRooms.Text = "";
                    textBoxFloor.Text = "";
                }
                //Если выбран Дом
                if (comboBoxType.SelectedIndex == 1)
                {
                    //И выбран элемент списка
                    if (listViewRealEstateSet_House.SelectedItems.Count == 1)
                    {
                        //Ищем элемент из таблицы по тегу
                        RealEstateSet realEstate = listViewRealEstateSet_House.SelectedItems[0].Tag as RealEstateSet;
                        //И удаляем его из базы данных
                        Program.wftDB.RealEstateSet.Remove(realEstate);
                        //Сохраняем изменения
                        Program.wftDB.SaveChanges();
                        //Обновляем список
                        ShowRealEstateSet();
                    }
                    //Очищаем поля формы
                    textBoxAddress_City.Text = "";
                    textBoxAddress_House.Text = "";
                    textBoxAddress_Street.Text = "";
                    textBoxAddress_Number.Text = "";
                    textBoxCoordinate_latitude.Text = "";
                    textBoxCoordinate_longitude.Text = "";
                    textBoxTotalArea.Text = "";
                    textBoxTotalFloors.Text = "";
                }
                //Если выбрана Земля
                else if (comboBoxType.SelectedIndex == 2)
                {
                    //И выбран элемент списка
                    if (listViewRealEstateSet_Land.SelectedItems.Count == 1)
                    {
                        //Ищем элемент из таблицы по тегу
                        RealEstateSet realEstate = listViewRealEstateSet_Land.SelectedItems[0].Tag as RealEstateSet;
                        //И удаляем его из базы данных
                        Program.wftDB.RealEstateSet.Remove(realEstate);
                        //Сохраняем изменения
                        Program.wftDB.SaveChanges();
                        //Обновляем список
                        ShowRealEstateSet();
                    }
                    //Очищаем поля формы
                    textBoxAddress_City.Text = "";
                    textBoxAddress_House.Text = "";
                    textBoxAddress_Street.Text = "";
                    textBoxAddress_Number.Text = "";
                    textBoxCoordinate_latitude.Text = "";
                    textBoxCoordinate_longitude.Text = "";
                    textBoxTotalArea.Text = "";
                }
            }
            //Если что-то пошло не так
            catch
            {
                MessageBox.Show("Невозможно удалить запись, возможно она используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ShowRealEstateSet()
        {
            //Очищаем listView
            listViewRealEstateSet_Apartment.Items.Clear();
            listViewRealEstateSet_House.Items.Clear();
            listViewRealEstateSet_Land.Items.Clear();
            //Проходим по коллекции недвижимости в базе с помощью foreach
            foreach (RealEstateSet realEstate in Program.wftDB.RealEstateSet)
            {
                //Квартира
                if (realEstate.Type == 0)
                {
                    //Создаём новый элемент для listViewRealEstateSet_Apartment из нового массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        realEstate.Address_City,
                        realEstate.Address_Street,
                        realEstate.Address_House,
                        realEstate.Address_Number,
                        realEstate.Coordinate_Latitude.ToString(),
                        realEstate.Coordinate_Longitude.ToString(),
                        realEstate.TotalArea.ToString(),
                        realEstate.Rooms.ToString(),
                        realEstate.Floor.ToString()
                    });
                    //Указываем по какому тегу будем брать элементы
                    item.Tag = realEstate;
                    //Добавляем клиента в listViewRealEstateSet_Apartment
                    listViewRealEstateSet_Apartment.Items.Add(item);
                }
                //Дом
                else if (realEstate.Type == 1)
                {
                    //Создаём новый элемент для listViewRealEstateSet_House из нового массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        realEstate.Address_City,
                        realEstate.Address_Street,
                        realEstate.Address_House,
                        realEstate.Address_Number,
                        realEstate.Coordinate_Latitude.ToString(),
                        realEstate.Coordinate_Longitude.ToString(),
                        realEstate.TotalArea.ToString(),
                        realEstate.TotalFloors.ToString(),
                    });
                    //Указываем по какому тегу будем брать элементы
                    item.Tag = realEstate;
                    //Добавляем клиента в listViewRealEstateSet_House
                    listViewRealEstateSet_House.Items.Add(item);
                }
                //Земля
                else if (realEstate.Type == 2)
                {
                    //Создаём новый элемент для listViewRealEstateSet_Land из нового массива строк
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        realEstate.Address_City,
                        realEstate.Address_Street,
                        realEstate.Address_House,
                        realEstate.Address_Number,
                        realEstate.Coordinate_Latitude.ToString(),
                        realEstate.Coordinate_Longitude.ToString(),
                        realEstate.TotalArea.ToString()
                    });
                    //Указываем по какому тегу будем брать элементы
                    item.Tag = realEstate;
                    //Добавляем клиента в listViewRealEstateSet_Land
                    listViewRealEstateSet_Land.Items.Add(item);
                }
            }
            //Выравниваем столбцы по ширине заголовка
            listViewRealEstateSet_Land.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRealEstateSet_House.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRealEstateSet_Apartment.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void listViewRealEstateSet_Land_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRealEstateSet_Land.SelectedItems.Count == 1)
            {
                //Ищем элемент из таблицы по тегу
                RealEstateSet realEstate = listViewRealEstateSet_Land.SelectedItems[0].Tag as RealEstateSet;
                //Обновляем данные на форме
                textBoxAddress_City.Text = realEstate.Address_City;
                textBoxAddress_House.Text = realEstate.Address_House;
                textBoxAddress_Street.Text = realEstate.Address_Street;
                textBoxAddress_Number.Text = realEstate.Address_Number;
                textBoxCoordinate_latitude.Text = realEstate.Coordinate_Latitude.ToString();
                textBoxCoordinate_longitude.Text = realEstate.Coordinate_Longitude.ToString();
                textBoxTotalArea.Text = realEstate.TotalArea.ToString();
                textBoxTotalFloors.Text = realEstate.TotalFloors.ToString();
            }
            else
            {
                //Если ничего не выбрано, очищаем поля
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxTotalFloors.Text = "";
            }
        }

        private void listViewRealEstateSet_House_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewRealEstateSet_House.SelectedItems.Count == 1)
            {
                //Ищем элемент из таблицы по тегу
                RealEstateSet realEstate = listViewRealEstateSet_House.SelectedItems[0].Tag as RealEstateSet;
                //Обновляем данные на форме
                textBoxAddress_City.Text = realEstate.Address_City;
                textBoxAddress_House.Text = realEstate.Address_House;
                textBoxAddress_Street.Text = realEstate.Address_Street;
                textBoxAddress_Number.Text = realEstate.Address_Number;
                textBoxCoordinate_latitude.Text = realEstate.Coordinate_Latitude.ToString();
                textBoxCoordinate_longitude.Text = realEstate.Coordinate_Longitude.ToString();
                textBoxTotalArea.Text = realEstate.TotalArea.ToString();
            }
            else
            {
                //Если ничего не выбрано, очищаем поля
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
            }
        }
    }
}
