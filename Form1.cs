using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace curs_v_1
{
    public partial class MyFile : Form
    {
        private Hash hashtable_ = new Hash();
        private AVL_tree root = new AVL_tree();
        private List<Key_1> avltree { get; set; }
        public MyFile()
        {
            hashtable_.Read_File(false);
            root.Readfile(true);
            InitializeComponent();
        }

        private void button1_AVL_Click(object sender, EventArgs e)// кнопка АВЛ дерева, рисует дерево в окне
        {
            AVL_tree_print.Clear();
            var avltree = new List<string>();
            avltree = root.Draw();

            AVL_tree_print.SelectedText = avltree[0];
            for (int i = 1; i < avltree.Count; i++)
            {
                AVL_tree_print.SelectedText = Environment.NewLine + avltree[i];
            }
        }

        private void AVL_tree_print_TextChanged_1(object sender, EventArgs e)//место, где рисуетса АВЛ
        {

        }

        private void Add_AVL_button_Click(object sender, EventArgs e)//кнопка добавления доставок в АВЛку 
        {
            string text1 = text_AVLadd_name.Text;
            string text2 = text_AVLadd_time.Text;
            string text3 = text_AVLadd_price.Text;
            // int auter_search = 0;
            int score = 0;
            if ((text1 != "") && (text2 != "") && (text3 != ""))
                if ((hashtable_.Search_fio(text1, ref score) >= 0)&&(!root.Seach_Delivery(text1,text2,text3)))
                {
                    if (root.Check(text1, text2, text3, true))
                    {
                        //Writ_table_AVL();
                        text_AVLadd_name.Text = "";
                        text_AVLadd_time.Text = "";
                        text_AVLadd_price.Text = "";
                       
                        MessageBox.Show("Доставка была добавлена", "Сообщение");
                    }
                    else MessageBox.Show("Введите корректные значения", "Сообщение");
                }
                else
                {
                    if (root.Seach_Delivery(text1, text2, text3)) { MessageBox.Show("Такая же доставка уже существует.", "Сообщение"); }
                    else MessageBox.Show("Такого курьера еще нет. Добавьте для начала курьера", "Сообщение");
                    text_AVLadd_name.Text = "";
                    text_AVLadd_time.Text = "";
                    text_AVLadd_price.Text = "";
                }
            else MessageBox.Show("Поля оказались пусты", "Сообщение");
        }

        private void Delete_AVL_button_Click(object sender, EventArgs e)// кнопка удаления ДОСТАВКИ в АВЛке
        {
            string text1 = text_AVLdelete_name.Text;
            string text2 = text_AVLdelete_time.Text;
            string text3 = text_AVLdelete_price.Text;

            if ((text1 != "") && (text2 != "") && (text3 != ""))
                if (root.CheckRot()) //не пустое дерево
                    if (root.Seach_Delivery(text1, text2, text3)) //такая книга есть
                    {
                        root.Delete(text1, text2, text3, true); //удаляем
                        MessageBox.Show("Доставка удалена", "Сообщение");
                        //Writ_table_AVL();
                        text_AVLdelete_name.Text = "";
                        text_AVLdelete_time.Text = "" ;
                        text_AVLdelete_price.Text = "";
                    }
                    else
                    {
                        text_AVLdelete_name.Text = "";
                        text_AVLdelete_time.Text = "";
                        text_AVLdelete_price.Text = "";
                        MessageBox.Show("Такой доставки нет, чтобы ее можно было удалить", "Сообщение");
                    }
                        
                else
                    MessageBox.Show("Нет ни одной доставки", "Сообщение");
            else
                MessageBox.Show("Поля оказались пусты", "Сообщение");

        }

        private void Share_AVL_button_Click(object sender, EventArgs e)//кнопка поиска в АВЛке по ФИО
        {
            string text1 = text_AVLshare_name.Text;
  
            var list = new List<string>();
            int score = 0;

            text_AVLshare_result.Clear();
            root.getSeach(text1, ref list);
            if (list.Count != 0)
            {
                text_AVLshare_result.SelectedText = list[0];
                for (int i = 1; i < list.Count; i++)
                {
                    text_AVLshare_result.SelectedText = Environment.NewLine + list[i];
                }
            }
            else if (hashtable_.Search_fio(text1, ref score) >=0) { MessageBox.Show("Нет ни одной доставки у данного курьера", "Сообщение"); }
                else if (text1=="") MessageBox.Show("Поля оказались пусты", "Сообщение");
                     else MessageBox.Show("Такого курьера не существует", "Сообщение");
        }
    
        private void button1_HT_Click(object sender, EventArgs e)//выводит ХТ в таблицу
        {
            HT_Table_print.Rows.Clear();
            HT_Table_print.Refresh();
            Notebook ht;
            avltree = root.CopiAVL();
            HT_Table_print.ColumnCount = 7;
            HT_Table_print.Columns[0].HeaderCell.Value = "№";
            HT_Table_print.Columns[1].HeaderCell.Value = "ХФ 1";
            HT_Table_print.Columns[2].HeaderCell.Value = "ХФ 2";
            HT_Table_print.Columns[3].HeaderCell.Value = "Статус";
            HT_Table_print.Columns[4].HeaderCell.Value = "ФИО";
            HT_Table_print.Columns[5].HeaderCell.Value = "Телефон";
            HT_Table_print.Columns[6].HeaderCell.Value = "Район доставки";

            for (int i = 0; i < 20; i++)
            {
                ht = hashtable_.Copielement(i);
                var index = HT_Table_print.Rows.Add();
                HT_Table_print.Rows[index].Cells[0].Value = i;
                HT_Table_print.Rows[index].Cells[1].Value = ht.index1;
                HT_Table_print.Rows[index].Cells[2].Value = ht.index2;
                HT_Table_print.Rows[index].Cells[3].Value = ht.state;
                HT_Table_print.Rows[index].Cells[4].Value = ht.fio;
                HT_Table_print.Rows[index].Cells[5].Value = ht.phone;
                HT_Table_print.Rows[index].Cells[6].Value = ht.district;
            }
        }

        private void Add_HT_button_Click(object sender, EventArgs e)//кнопка добавления элемента в ХТ 
        {
            string text1 = text_HTadd_name.Text;
            string text2 = text_HTadd_phone.Text;
            string text3 = text_HTadd_district.Text;
            int score = 0;
            if (text1 == "" || text2 == "" || text3 == "")
            {
                MessageBox.Show("Поля оказались пусты", "Сообщение");
            }
            else if (hashtable_.Check_(text1, text2, text3) && (hashtable_.Search_fio(text1, ref score) < 0))
            {
                hashtable_.Check(text1, text2, text3);
                text_HTadd_name.Text = "";
                text_HTadd_phone.Text = "";
                text_HTadd_district.Text = "";
                MessageBox.Show("Курьер был успешно добавлен.", "Сообщение");
            }
            else if (hashtable_.Search_fio(text1, ref score) > 0)
            {
                text_HTadd_name.Text = "";
                
                MessageBox.Show("Такой курьер уже существует.", "Сообщение");
            }
            else if (!hashtable_.Check_(text1, text2, text3)) MessageBox.Show("Введите корректные значения", "Сообщение");
        }

        private void Delete_HT_button_Click(object sender, EventArgs e)
        {
            string text1 = text_HTdelete_name.Text;
          
            int score = 0;

            if (text1 == "")//если поля пусты то вывести сообщение
            {
                MessageBox.Show("Поля оказались пусты", "Сообщение");
            }
            else if (hashtable_.Search_fio(text1, ref score) >= 0)//если такой элемент существует
            {
                if (hashtable_.Del(text1))
                {
                    
                    text_HTdelete_name.Text = "";
                
                    root.Delete_fio(text1, true);
                    MessageBox.Show("Курьер был успешно удален.", "Сообщение");
                }
                else  MessageBox.Show("Данный курьер не доставляет в данный район.", "Сообщение");
            }
            else if (hashtable_.Search_fio(text1, ref score) < 0)
            {
                MessageBox.Show("Такого курьера не существует.", "Сообщение");
            }
        }

        private void Share_HT_button_Click(object sender, EventArgs e)
        {
            string text1 = text_HTshare_name.Text;
           
            Notebook ht;
            int score = 0;
            if (text1 == "") MessageBox.Show("Поле оказалось пустым", "Сообщение");
            else
            {
                if (hashtable_.Search_fio(text1, ref score) >= 0)
                {
                    int index = hashtable_.Search_fio(text1, ref score);
                    ht = hashtable_.Copielement(index);
                    text_HTshare_phone.Text = ht.phone;
                    text_HTshare_district.Text = ht.district;

                }
                else
                {
                    text_HTshare_phone.Text = "";
                    text_HTshare_district.Text = "";
                    MessageBox.Show("Такого курьера не существует", "Сообщение");
                }
            }
        }

        private void REPORT_button_Click(object sender, EventArgs e)
        {
              string text1 = text_report_district.Text;
            var list = new List<string>();
            var list_ = new List<string>();
            int score = 0;
            REPORT_result.Clear();
            
            if (text1 == "") MessageBox.Show("Поле оказалось пустым", "Сообщение");
            else
            {
                if (hashtable_.root_.Search_dis(text1))
                {
                    hashtable_.getReport(text1, ref score, ref list, ref list_, root);
                    if (list.Count != 0)
                    {
                        REPORT_result.SelectedText = list[0];
                        for (int i = 1; i < list.Count; i++)
                        {
                            REPORT_result.SelectedText = Environment.NewLine + list[i];
                        }
                    }

                }
                else MessageBox.Show("Такого района не существует", "Сообщение");
            }

          /*REPORT_result.Clear();
            
            var avltree = new List<string>();
            avltree = hashtable_.root_.Draw();

            REPORT_result.SelectedText = avltree[0];
            for (int i = 1; i < avltree.Count; i++)
            {
                REPORT_result.SelectedText = Environment.NewLine + avltree[i];
            }*/
        }
    }
}
