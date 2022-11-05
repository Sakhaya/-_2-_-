using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace curs_v_1
{
	public struct Notebook
	{
		public string fio, phone, district;
		public int state, index1, index2;
	};

	class Hash
	{
		private Notebook[] table; //таблица
		private int buffer_size; //размер таблицы
		//public AVL_tree_d root_;
		public AVL_tree_d root_ = new AVL_tree_d();
		private int Hash_One_Function(string key) //первичная хеш-функция  ++++
		{
			int size_key = key.Length;
			int summ = 0;
			int buff;
			for (int i = 0; i < size_key; i++)
			{
				buff = Convert.ToInt32(key[i]) + 128; //зачение буквы !!!!!!!!!!!!!!!!!!!!!!
				Console.Write(buff + "   ");
				summ += buff;
			}
			Console.WriteLine();
			return (summ % buffer_size);
		}

		private int Hash_Two_Function(int index, int j) //++++
		{
			return ((index + j * 1) % buffer_size);
		}

		private int Collision(int index, string buff) //++++
		{
			int j = 1;
			int h2 = Hash_Two_Function(index, j);
			while (table[h2].state == 1 && j < buffer_size)
			{
				++j;
				h2 = Hash_Two_Function(index, j);
			}
			if (j < buffer_size)
				return h2;
			else
				return -1;
		}

		private int Collision_SH(int index, string buff) //++++
		{

			int j = 1;
			int h2 = Hash_Two_Function(index, j);
			while (table[h2].state != 1 || table[h2].district!=buff && j < buffer_size)
			{
				++j;
				h2 = Hash_Two_Function(index, j);
				if (h2 == 19)
                {
					return -1;
                }
			}
			if (j < buffer_size && table[h2].district == buff)
				return h2;
			else
				return -1;
		}

		private int Decision(int index, string fio_v) //-----
		{
			int two_index = index;
			int i = 0;
			int j = 1;
			while (i < buffer_size)
			{
				two_index = Hash_Two_Function(index, j);
				if (table[two_index].fio == fio_v && table[two_index].state == 1) return two_index;
				i++;
				j++;

				Console.Write(two_index);
				Console.WriteLine(" index del");
			}
			return -1;
		}

		private bool Check_Letter(string value) //проверяет буквы на коректность
		{
			int i, j;
			j = 0;
			i = value.Length;
			do
			{
				if (((value[j] >= 'а') && (value[j] <= 'я')) || ((value[j] >= 'А') && (value[j] <= 'Я')) || (value[j] == ' ') || (value[j] == '-') || ((value[j] >= 'A') && (value[j] <= 'Z')) || ((value[j] >= 'a') && (value[j] <= 'z')))
					j++;
				else
					return false;
			} while (j != i);
			return true;
		}

		private bool Check_Number(string value) //проверяет стоимость на коректность
		{
			int i, j;
			j = 0;
			i = value.Length;
			
			do
			{
				if ((value[j] >= '0') && (value[j] <= '9'))
					j++;
				else
					return false;
			} while (j != i);
			
			long a = Convert.ToInt64(value);

			if (a >= 80000000000 && a <= 89999999999) return true;
			else return false;
			
		}

		public int Size_table()
		{
			return buffer_size;
		}

		public Notebook Copielement(int index)
		{
			return table[index];
		}

		public Hash() //конструктор ++++
		{
			buffer_size = 20; //размер таблицы
			table = new Notebook[buffer_size]; //сама таблица

			for (int i = 0; i < buffer_size; i++)
			{
				table[i].fio = table[i].phone = table[i].district = "_"; //заполняем таблицу пробелами
				table[i].state = 0; table[i].index1 = -1; table[i].index2 = -1;
			}
		}

		private bool Add(string fio_v, string phone_v, string district_v, int x) //добавление  !!!!!!!!!!!!!!!  ++++
		{
			int index;
			if (x == 0) { index = Hash_One_Function(district_v); }
			else index = x;

			if (table[index].state != 1)
			{
				table[index].phone = phone_v;
				table[index].fio = fio_v;
				table[index].district = district_v;
				table[index].state = 1;
				table[index].index1 = index;
				table[index].index2 = -1;
				root_.Check(district_v, fio_v, true);
				return true;
			}
			else 
			{

				int two_index = Collision(index, district_v);
				if (two_index >= 0 && table[two_index].state != 1)
				{
					table[two_index].phone = phone_v;
					table[two_index].fio = fio_v;
					table[two_index].district = district_v;
					table[two_index].state = 1;
					table[two_index].index1 = index;
					table[two_index].index2 = two_index;
					root_.Check(district_v, fio_v, true);
					return true;
				}
				else if (two_index >= 0 && table[two_index].state == 1) { Add(fio_v, phone_v, district_v, two_index); root_.Check(district_v, fio_v, true); return false; }
				else return false;
			}
			
		}


		public bool Check(string fio_v, string phone_v, string district_v) //добавляет элемент в таблицу и проверяет на корректонсть элементы
		{
			if (Check_Letter(fio_v) && Check_Number(phone_v)&& Check_Letter(district_v))
            {
				
					if (Add(fio_v, phone_v, district_v, 0)) 
					{
						//root_.Check(district_v, fio_v, true);
						return true; 
					} 
				
			}
			return false;
		}

		public bool Check_(string fio_v, string phone_v, string district_v) //проверка на корректность введенных значений
		{
			if (Check_Letter(fio_v) && Check_Number(phone_v) && Check_Letter(district_v))
			{
				return true;
			}
			return false;
		}

		public bool Del(string fio_v) //удаление  ++++
		{
			int index1 = Hash_One_Function(fio_v);

			if (table[index1].fio == fio_v && table[index1].state == 1)
			{
				root_.Delete(table[index1].district, table[index1].fio, true);
				table[index1].state = 2;
				
				return true;
			}
			else
			{
				int two_index = Decision(index1, fio_v);
				if (two_index >= 0)
				{
					root_.Delete(table[two_index].district, table[two_index].fio, true);
					table[two_index].state = 2;
					
					return true;
				}
				else return false;
			}
		}

		public void getReport(string district_v, ref int score, ref List<string> list, ref List<string> list_, AVL_tree tree)
		{
			string buffer = "";
			score = 0;
			int last = 0;
			var tree_list = new List<string>();
			
			root_.getSeach(district_v, ref tree_list);

			for (int i = 0; i < tree_list.Count; i++)
            {
				tree.getReport(tree_list[i], ref list_);

				last = Search_fio(tree_list[i], ref score);
				
				buffer = buffer + Environment.NewLine + table[last].fio + " " + table[last].phone;
				for (int j = 0; j < list_.Count; j++)
				{
					buffer = buffer + Environment.NewLine + list_[j];
				}
				list.Add(buffer);
				list_.Clear();
				buffer = "";

            }

		}

		public int Search_fio(string fio_v, ref int score) //поиск в ХТ по ФИО
		{
			int index = Hash_One_Function(fio_v);
			int two_index = index;
			int i = 0;
			int j = 1;
			score = 0;
			while (table[two_index].fio != " " && i < buffer_size)
			{
				score++;
				if (table[two_index].fio == fio_v && table[two_index].state == 1) return two_index;
				two_index = Hash_Two_Function(index, j);
				j++;
				i++;
			}
			return -1;
		}
		
		public void Read_File(bool prov)
		{
			string curfile = @"Courier.txt";

			if (File.Exists(curfile))
			{

				StreamReader file = new StreamReader("Courier.txt");

				string fio_v, phone_v, district_v, buff;
				fio_v = phone_v = district_v = "";

				while (!file.EndOfStream)
				{
					buff = file.ReadLine();
					string litter = "*";
					int j = 0;

					for (int i = 0; i < 2; i++)
					{
						while (buff[j] != litter[0]) j++;
						if (i == 0) fio_v = buff.Substring(0, j);
						if (i == 1) phone_v = buff.Substring(0, j);

						buff = buff.Remove(0, j + 1);
						j = 0;
					}
					int size_buff = buff.Length;
					district_v = buff.Substring(0, size_buff);

					Add(fio_v, phone_v, district_v, 0);
					

					//root_.Check(district_v, fio_v,true);
				}
				file.Close();
			}
		}

		public void Write_file()
		{
			string curfile = @"Courier.txt";

			if (File.Exists(curfile))
			{
				File.WriteAllText(curfile, ""); //очистка файла
				StreamWriter file = new StreamWriter("Courier.txt");


				for (int i = 0; i < buffer_size; i++)
				{
					if (table[i].fio != "_") file.WriteLine(table[i].phone + "*" + table[i].fio + "*" + table[i].district);
				}
				file.Close();
			}
		}

		public void Write_History(int index)
		{
			string curfile = @"History.txt";

			if (File.Exists(curfile))
			{
				File.WriteAllText(curfile, ""); //очистка файла
				StreamWriter file = new StreamWriter("History.txt");
				file.WriteLine(table[index].phone + "  " + table[index].fio + "  " + table[index].district);
				file.Close();
			}
		}
		~Hash() //деструктор
		{

		}
	};
}
