using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace curs_v_1
{
	public class Key_1 //класс ключа
	{
		public Key_1 next;
		public string fio, time,price;
		 //fio, time, price;
		public Key_1(string fio_v, string time_v, string price_v)
		{
			next = null;
			fio = fio_v;
			time = time_v;
			price = price_v;
		}
	};
	

	class AVL_tree
	{
		class Node //класс дерева
		{
			public int balans;
			public Key_1 key;
			public Node left;
			public Node right;

			public Node(string fio_v, string time_v, string price_v)
			{
				left = null;
				right = null;
				balans = 0;
				key = new Key_1(fio_v, time_v, price_v);
			}
		};

		Node root = null; //корень
		
		private void Add(ref Node tree, string fio_v, string time_v, string price_v, ref bool flag) //добавление
		{
			Key_1 vertex;
			Node tree1;
			Node tree2;
			if (!Seach_Delivery(fio_v, time_v, price_v))
			{
				if (tree == null)
					tree = new Node(fio_v, time_v, price_v);
				else if ((string.Compare(tree.key.fio, fio_v) > 0))
				{
					Add(ref tree.left, fio_v, time_v, price_v, ref flag);

					if (flag)
						switch (tree.balans)
						{
							case 1: tree.balans = 0; flag = false; break;
							case 0: tree.balans = -1; break;
							case -1:
								tree1 = tree.left;
								if (tree1.balans == -1)
								{
									tree.left = tree1.right;
									tree1.right = tree;
									tree.balans = 0;
									tree = tree1;
								}
								else
								{
									tree2 = tree1.right;
									tree1.right = tree2.left;
									tree2.left = tree1;
									tree.left = tree2.right;
									tree2.right = tree;
									if ((tree.left != null && tree.right != null) || (tree.left == null && tree.right == null))
										tree.balans = 0;
									else
										if (tree.left != null)
										tree.balans = -1;
									else
										if (tree.right != null)
										tree.balans = 1;

									if ((tree1.left != null && tree1.right != null) || (tree1.left == null && tree1.right == null))
										tree1.balans = 0;
									else
										if (tree1.left != null)
										tree1.balans = -1;
									else
										if (tree1.right != null)
										tree1.balans = 1;
									tree = tree2;
								}
								tree.balans = 0;
								flag = false;
								break;
						}
				}
				else if ((string.Compare(tree.key.fio, fio_v) < 0))
				{
					Add(ref tree.right, fio_v, time_v, price_v, ref flag);

					if (flag)
						switch (tree.balans)
						{
							case -1: tree.balans = 0; flag = false; break;
							case 0: tree.balans = 1; break;
							case 1:
								tree1 = tree.right;
								if (tree1.balans == 1)
								{
									tree.right = tree1.left;
									tree1.left = tree;
									tree.balans = 0;
									tree = tree1;
								}
								else
								{
									tree2 = tree1.left;
									tree1.left = tree2.right;
									tree2.right = tree1;
									tree.right = tree2.left;
									tree2.left = tree;
									if ((tree.left != null && tree.right != null) || (tree.left == null && tree.right == null))
										tree.balans = 0;
									else
										if (tree.left != null)
										tree.balans = -1;
									else
										if (tree.right != null)
										tree.balans = 1;

									if ((tree1.left != null && tree1.right != null) || (tree1.left == null && tree1.right == null))
										tree1.balans = 0;
									else
										if (tree1.left != null)
										tree1.balans = -1;
									else
										if (tree1.right != null)
										tree1.balans = 1;

									tree = tree2;
								}
								tree.balans = 0;
								flag = false;
								break;
						}
				}
				else
					if (tree.key.next == null)
				{
					vertex = new Key_1(fio_v, time_v, price_v);
					tree.key.next = vertex;
					flag = false;
				}
				else if (tree.key.fio == fio_v)//если совпадение по имени, то список по цепочке
				{
					vertex = new Key_1(fio_v, time_v, price_v);
					Key_1 buff;
					buff = tree.key;
					while (buff.next != null) buff = buff.next;
					buff.next = vertex;
					flag = false;
				}
				Write_file();
			}
		}

		private void BalanceL(ref Node tree, ref bool flag) //баланс при удалении
		{
			Node tree1;
			Node tree2;

			switch (tree.balans)
			{
				case -1: tree.balans = 0; break;
				case 0: tree.balans = 1; flag = false; break;
				case 1:
					tree1 = tree.right;
					if (tree1.balans >= 0)
					{
						tree.right = tree1.left;
						tree1.left = tree;
						if (tree1.balans == 0)
						{
							tree.balans = 1;
							tree1.balans = -1;
							flag = false;
						}
						else
						{
							tree.balans = 0;
							tree1.balans = 0;
						}
						tree = tree1;
					}
					else
					{
						tree2 = tree1.left;
						tree1.left = tree2.right;
						tree2.right = tree1;
						tree.right = tree2.left;
						tree2.left = tree;
						if (tree2.balans == 1)
							tree.balans = -1;
						else
							tree.balans = 0;

						if (tree2.balans == -1)
							tree1.balans = 1;
						else
							tree1.balans = 0;

						tree = tree2;
						tree2.balans = 0;
					}
					break;
			}
		}

		private void BalanceR(ref Node tree, ref bool flag) //баланс при удалении
		{
			Node tree1;
			Node tree2;

			switch (tree.balans)
			{
				case 1: tree.balans = 0; break;
				case 0: tree.balans = -1; flag = false; break;
				case -1:
					tree1 = tree.left;
					if (tree1.balans <= 0)
					{
						tree.left = tree1.right;
						tree1.right = tree;
						if (tree1.balans == 0)
						{
							tree.balans = -1;
							tree1.balans = 1;
							flag = false;
						}
						else
						{
							tree.balans = 0;
							tree1.balans = 0;
						}
						tree = tree1;
					}
					else
					{
						tree2 = tree1.right;
						tree1.right = tree2.left;
						tree2.left = tree1;
						tree.left = tree2.right;
						tree2.right = tree;
						if (tree2.balans == -1)
							tree.balans = 1;
						else
							tree.balans = 0;

						if (tree2.balans == 1)
							tree1.balans = -1;
						else
							tree1.balans = 0;

						tree = tree2;
						tree2.balans = 0;
					}
					break;
			}
		}

		private void DeleteTwo(ref Node r, ref bool flag, ref Node q) //
		{
			if (r.left != null)
			{
				DeleteTwo(ref r.left, ref flag, ref q);
				if (flag) BalanceL(ref r, ref flag);
			}
			else
			{
				q.key.fio = r.key.fio;
				q.key.time = r.key.time;
				q.key.price = r.key.price;
				q.key.next = r.key.next;
				q = r;
				r = r.right;
				flag = true;
			}
		}

		private void DeleteOne(ref Node tree, ref bool flag, string fio_v, string time_v, string price_v) //удаляет доставку
		{
			Node q;

			if (tree != null)
				if ((tree.key.fio == fio_v) && (tree.key.next != null)) //
				{
					Key_1 pointer = tree.key;

					if ((pointer.time == time_v)&&(pointer.price == price_v))
					{
						tree.key = tree.key.next;
						pointer.next = null;
						flag = false;
					}
					else
					{
						do
						{
							pointer = pointer.next;
							if ((pointer.time == time_v) && (pointer.price == price_v)) //
							{
								Key_1 pointer1 = tree.key;
								while (pointer1.next != pointer) pointer1 = pointer1.next; //
								pointer1.next = pointer.next;
								pointer = null;
								pointer = pointer1;
								flag = false;
							}
						} while (pointer.next != null);
						if ((pointer.next == null) && (pointer.time == time_v) && (pointer.price == price_v)) //
						{
							Key_1 pointer1 = tree.key;
							while (pointer1.next != pointer) pointer1 = pointer1.next; //
							pointer1.next = null;
							pointer = pointer1;
							flag = false;
						}
					}
				}
				else if ((string.Compare(tree.key.fio, fio_v) > 0))
				{
					DeleteOne(ref tree.left, ref flag, fio_v, time_v, price_v);
					if (flag) BalanceL(ref tree, ref flag);
				}
				else if ((string.Compare(tree.key.fio, fio_v) < 0))
				{
					DeleteOne(ref tree.right, ref flag, fio_v, time_v, price_v);
					if (flag) BalanceR(ref tree, ref flag);
				}
				else
				{
					q = tree;
					if (q.right == null)
					{
						tree = q.left;
						flag = true;
					}
					else if (q.left == null)
					{
						tree = q.right;
						flag = true;
					}
					else
					{
						DeleteTwo(ref q.right, ref flag, ref q);
						if (flag) BalanceR(ref tree, ref flag); //!!!!!!!!!!
					}
				}
			Write_file();
		}

		private void DeleteOne_fio(ref Node tree, ref bool flag, string fio_v) //удаляет доставку
		{
			Node q;

			if (tree != null)
				if (string.Compare(tree.key.fio, fio_v) > 0)
				{
					DeleteOne_fio(ref tree.left, ref flag, fio_v);
					if (flag) BalanceL(ref tree, ref flag);
				}
				else if (string.Compare(tree.key.fio, fio_v) < 0)
				{
					DeleteOne_fio(ref tree.right, ref flag, fio_v);
					if (flag) BalanceR(ref tree, ref flag);
				}
				else
				{
					q = tree;
					if (q.right == null)
					{
						tree = q.left;
						flag = true;
					}
					else if (q.left == null)
					{
						tree = q.right;
						flag = true;
					}
					else
					{
						DeleteTwo(ref q.right, ref flag, ref q);
						if (flag) BalanceR(ref tree, ref flag); //!!!!!!!!!!
					}
				}
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

			int a = Convert.ToInt32(value);

			if (a >= 0 && a <= 10000) return true;
			else return false;
		}

		private bool Check_Data(string value) //проверяет дату на коректность
		{

			int i, j;
			j = 0;
			i = value.Length;
			if (i != 16) return false;
			else
			do
			{
				if (((value[j] >= '0') && (value[j] <= '9'))||(value[2] == '.') || (value[5] == '.') || (value[13] == ':')||(value[10] == '/'))
					j++;
				else
					return false;
			} while (j != i);
			return true;
		}

		private bool Check_Letter(string value) //проверяет буквы на коректность
		{
			int i, j;
			j = 0;
			i = value.Length;

			do
			{
				//Console.Write(value);
				if (((value[j] >= 'а') && (value[j] <= 'я')) || ((value[j] >= 'А') && (value[j] <= 'Я')) || (value[j] == ' ') || ((value[j] >= 'A') && (value[j] <= 'Z')) || ((value[j] >= 'a') && (value[j] <= 'z')))
					j++;
				else
					return false;
			} while (j != i);
			return true;
		}

		private void Cleaning(ref Node tree) //очистка
		{
			if (tree != null)
			{
				Cleaning(ref tree.left);
				Cleaning(ref tree.right);
				tree = null;
			}
		}

		private void Drawing(ref Node tree, int h, ref List<string> list) //выписывание
		{
			string buff = "";
			if (tree != null)
			{
				h += 3;
				Drawing(ref tree.right, h, ref list);


				if (tree.key.next == null)
				{
					for (int i = 0; i < h; i++) buff = buff + "  ";
					buff = buff + tree.balans + " " + tree.key.fio + " " + tree.key.time + " " + tree.key.price + "   ";
					list.Add(buff);
				}
				else 
				{
					for (int i = 0; i < h; i++) buff = buff + "  ";
					buff = buff + tree.balans + " " + tree.key.fio + " " + tree.key.time + " " + tree.key.price + "   ";

					Key_1 buff1 = tree.key;
					do
					{
						buff1 = buff1.next;
						buff = buff + " " + buff1.time + " " + buff1.price + "   ";

					} while (buff1.next != null);
					list.Add(buff);
				}
				Drawing(ref tree.left, h, ref list);
			}
		}

		public bool Seach_Delivery(string fio_v, string time_v, string price_v) //поиск доставки !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		{
			Node curr = root;
			while (curr != null)
			{
				if (fio_v == curr.key.fio && time_v == curr.key.time && price_v == curr.key.price) return true;
				if (fio_v == curr.key.fio && curr.key.next != null)
				{
					Key_1 buff = curr.key;
					do
					{
						buff = buff.next;
						if (price_v == buff.price && time_v == buff.time) return true;
					} while (buff.next != null);
				}
				if ((string.Compare(curr.key.fio, fio_v) > 0)) curr = curr.left;
				else curr = curr.right;
			}
			return false;
		}

		public void getSeach(string fio_v, ref List<string> list)
		{
			string buffer = "";
			Node curr = root;
			while (curr != null)
			{
				if (fio_v == curr.key.fio)
				{
					buffer = buffer + curr.key.fio + " " + curr.key.time + " " + curr.key.price;
					list.Add(buffer);
					buffer = "";
				}
				if (fio_v == curr.key.fio && curr.key.next != null)
				{
					Key_1 buff = curr.key;
					do
					{
						buff = buff.next;
						buffer = buffer + buff.fio + " " + buff.time + " " + buff.price;
						list.Add(buffer);
						buffer = "";
					} while (buff.next != null);
				}
				if ((string.Compare(curr.key.fio, fio_v) > 0)) curr = curr.left;
				else curr = curr.right;
			}
		}

		public void getReport(string fio_v, ref List<string> list)
		{
			string buffer = "";
			Node curr = root;
			while (curr != null)
			{
				if (fio_v == curr.key.fio)
				{
					buffer = buffer + curr.key.time + " " + curr.key.price;
					list.Add(buffer);
					buffer = "";
				}
				if (fio_v == curr.key.fio && curr.key.next != null)
				{
					Key_1 buff = curr.key;
					do
					{
						buff = buff.next;
						buffer = buffer + buff.time + " " + buff.price;
						list.Add(buffer);
						buffer = "";
					} while (buff.next != null);
				}
				if ((string.Compare(curr.key.fio, fio_v) > 0)) curr = curr.left;
				else curr = curr.right;
			}
		}
		public bool Search_fio(string fio_v) //поиск фамилии
		{
			Node curr = root;
			while (curr != null)
			{
				if (fio_v == curr.key.fio) return false;
				if (string.Compare(curr.key.fio, fio_v) > 0) curr = curr.left;
				else curr = curr.right;
			}
			return true;
		}

		private void Rbl(ref Node tree, ref List<Key_1> list) //
		{
			if (tree != null)
			{
				Rbl(ref tree.right, ref list);
				if (tree.key.next == null)
				{
					list.Add(tree.key);
				}
				else
				{
					Key_1 buff = tree.key;
					list.Add(buff);
					do
					{
						buff = buff.next;
						list.Add(buff);
					} while (buff.next != null);
				}
				Rbl(ref tree.left, ref list);
			}
		}

		public bool Check(string fio_v, string time_v, string price_v, bool flag) //добавление
		{
			
			if (Check_Number(price_v) && Check_Letter(fio_v) && Check_Data(time_v))
			{
				if (root == null)
					root = new Node(fio_v, time_v, price_v);
				else
					Add(ref root, fio_v, time_v, price_v, ref flag);
				return true;
			}
			else
			{
				return false;
			}
		}

		~AVL_tree() //
		{
			Cleaning(ref root);
		}

		public void Delete(string fio_v, string time_v, string price_v, bool flag) 
		{
			DeleteOne(ref root, ref flag, fio_v, time_v, price_v);
		}

		public void Delete_fio(string fio_v, bool flag) 
		{
			while (!Search_fio(fio_v))
				DeleteOne_fio(ref root, ref flag, fio_v);
		}

		public bool CheckRot()
		{
			if (root != null)
				return true;
			else return false;
		}

		public void Readfile(bool flag) //читает данные из файла
		{
			string curfile = @"Delivery.txt";

			if (File.Exists(curfile))//проверка
			{
				StreamReader file = new StreamReader("Delivery.txt");
				string fio_v, time_v, price_v, buff;
				fio_v = time_v = price_v = "";

				while (!file.EndOfStream)
				{
					buff = file.ReadLine();
					string litter = "*";
					int j = 0;

					for (int i = 0; i < 2; i++)
					{
						while (buff[j] != litter[0]) j++;
						if (i == 0) fio_v = buff.Substring(0, j);
						if (i == 1) time_v = buff.Substring(0, j);
						

						buff = buff.Remove(0, j + 1);
						j = 0;
					}
					int size_buff = buff.Length;
					price_v = buff.Substring(0, size_buff);

					Check(fio_v, time_v, price_v, flag);
				}
				file.Close();
			}
		}

		public void Write_file()
		{
			string curfile = @"Delivery_.txt";

			if (File.Exists(curfile))//проверка
			{
				File.WriteAllText(curfile, ""); //очистка файла
				StreamWriter file = new StreamWriter("Delivery_.txt");

				var avltree = new List<Key_1>();
				Rbl(ref root, ref avltree);

				for (int i = 0; i < avltree.Count; i++)
				{
					file.WriteLine(avltree[i].fio + "*" + avltree[i].time + "*" + avltree[i].price);
				}

				file.Close();
			}
		}

		public AVL_tree() //конструктор
		{

		}

		public void Clear() //очищает дерево
		{
			Cleaning(ref root);
		}

		public List<string> Draw() //выписывает дерево
		{
			var avltree = new List<string>();

			if (root == null)
			{
				string buff = "Элементов нет";
				avltree.Add(buff);
				return avltree;
			}
			else
			{
				Drawing(ref root, 4, ref avltree); //изменяет массив.
				return avltree;
			}
		}

		public List<Key_1> CopiAVL()
		{
			var avltree = new List<Key_1>();
			Rbl(ref root, ref avltree);
			return avltree;
		}
	};
}
