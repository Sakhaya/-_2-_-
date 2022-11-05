using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace curs_v_1
{
	public class Key_2 //класс ключа
	{
		public Key_2 next;
		public string district, fio;

		public Key_2(string district_v, string fio_v)
		{
			next = null;
			district = district_v;
			fio = fio_v;
		}
	};

	class AVL_tree_d
	{
		class Node //класс дерева
		{
			public int balans;
			public Key_2 key;
			public Node left;
			public Node right;

			public Node(string district_v, string fio_v)
			{
				left = null;
				right = null;
				balans = 0;
				key = new Key_2(district_v,fio_v);
			}
		};

		Node root = null; //корень

		private void Add(ref Node tree, string district_v, string fio_v, ref bool flag) //добавление
		{
			Key_2 vertex;
			Node tree1;
			Node tree2;
			
				if (tree == null)
					tree = new Node(district_v, fio_v);
				else if ((string.Compare(tree.key.district, district_v) > 0))
				{
					Add(ref tree.left, district_v, fio_v, ref flag);

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
				else if ((string.Compare(tree.key.district, district_v) < 0))
				{
					Add(ref tree.right, district_v, fio_v, ref flag);

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
					vertex = new Key_2(district_v, fio_v);
					tree.key.next = vertex;
					flag = false;
				}
				else if (tree.key.district == district_v)//если совпадение по имени, то список по цепочке
				{
					vertex = new Key_2(district_v, fio_v);
					Key_2 buff;
					buff = tree.key;
					while (buff.next != null) buff = buff.next;
					buff.next = vertex;
					flag = false;
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
				q.key.district = r.key.district;
				q.key.next = r.key.next;
				q = r;
				r = r.right;
				flag = true;
			}
		}

		private void DeleteOne(ref Node tree, ref bool flag, string district_v, string fio_v) //удаляет доставку
		{
			Node q;

			if (tree != null)
				if ((tree.key.district == district_v) && (tree.key.next != null)) //
				{
					Key_2 pointer = tree.key;

					if ((pointer.fio == fio_v))
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
							if (pointer.fio == fio_v) //
							{
								Key_2 pointer1 = tree.key;
								while (pointer1.next != pointer) pointer1 = pointer1.next; //
								pointer1.next = pointer.next;
								pointer = null;
								pointer = pointer1;
								flag = false;
							}
						} while (pointer.next != null);
						if ((pointer.next == null) && (pointer.fio == fio_v)) //
						{
							Key_2 pointer1 = tree.key;
							while (pointer1.next != pointer) pointer1 = pointer1.next; //
							pointer1.next = null;
							pointer = pointer1;
							flag = false;
						}
					}
				}
				else if ((string.Compare(tree.key.district, district_v) > 0))
				{
					DeleteOne(ref tree.left, ref flag, district_v, fio_v);
					if (flag) BalanceL(ref tree, ref flag);
				}
				else if ((string.Compare(tree.key.district, district_v) < 0))
				{
					DeleteOne(ref tree.right, ref flag, district_v, fio_v);
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
					buff = buff + tree.balans + " " + tree.key.district + " " + tree.key.fio + "   ";
					list.Add(buff);
				}
				else
				{
					for (int i = 0; i < h; i++) buff = buff + "  ";
					buff = buff + tree.balans + " " + tree.key.district + " " + tree.key.fio  + "   ";

					Key_2 buff1 = tree.key;
					do
					{
						buff1 = buff1.next;
						buff = buff + " " + buff1.fio + "   ";

					} while (buff1.next != null);
					list.Add(buff);
				}
				Drawing(ref tree.left, h, ref list);
			}
		}

		public void getSeach(string district_v, ref List<string> list)
		{
			string buffer = "";
			Node curr = root;
			while (curr != null)
			{
				if (district_v == curr.key.district)
				{
					buffer = buffer + curr.key.fio;
					list.Add(buffer);
					buffer = "";
				}
				if (district_v == curr.key.district && curr.key.next != null)
				{
					Key_2 buff = curr.key;
					do
					{
						buff = buff.next;
						buffer = buffer + buff.fio;
						list.Add(buffer);
						buffer = "";
					} while (buff.next != null);
				}
				if ((string.Compare(curr.key.district, district_v) > 0)) curr = curr.left;
				else curr = curr.right;
			}
		}
		public bool Search_dis(string district_v) //поиск района
		{
			Node curr = root;
			while (curr != null)
			{
				if (district_v == curr.key.district) return true;
				if (string.Compare(curr.key.district, district_v) > 0) curr = curr.left;
				else curr = curr.right;
			}
			return false;
		}

		private void Rbl(ref Node tree, ref List<Key_2> list) //
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
					Key_2 buff = tree.key;
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

		public bool Check(string district_v, string fio_v, bool flag) //добавление
		{

			if (Check_Letter(fio_v) && Check_Letter(district_v))
			{
				if (root == null)
					root = new Node(district_v, fio_v);
				else
					Add(ref root, district_v, fio_v, ref flag);
				return true;
			}
			else
			{
				return false;
			}
		}

		~AVL_tree_d() //
		{
			Cleaning(ref root);
		}

		public void Delete(string district_v, string fio_v, bool flag)
		{
			DeleteOne(ref root, ref flag, district_v, fio_v);
		}

		public AVL_tree_d() //конструктор
		{

		}

	};
}
