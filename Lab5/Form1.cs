using Lab5;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            gvTV.AutoGenerateColumns = false;

            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Model";
            column.Name = "Модель";
            gvTV.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Brand";
            column.Name = "Бренд";
            gvTV.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "ScreenSize";
            column.Name = "Розмір";
            gvTV.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Resolution";
            column.Name = "Розширення";
            gvTV.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "IsSmart";
            column.Name = "Смарт";
            gvTV.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Color";
            column.Name = "Колір";
            gvTV.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Price";
            column.Name = "Ціна";
            gvTV.Columns.Add(column);

            bindSrcTV.Add(new TV("A21", "Samsung", 50, "1024x256", false, "чорний", 80000));
            EventArgs args = new EventArgs(); OnResize(args);

        }

        private void fMain_Resize(object senderun, EventArgs e)
        {
            int buttonsSize = 9 * btnAdd.Width + 3 * tsSeparator1.Width + 30;
            btnOpenFromText.Margin = new Padding(Width - buttonsSize, 0, 0, 0);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TV tv = new TV();
            fTV fTV = new fTV(ref tv);
            if (fTV.ShowDialog() == DialogResult.OK)
            {
                bindSrcTV.Add(tv);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            TV tv = (TV)bindSrcTV.List[bindSrcTV.Position];

            fTV fTV = new fTV(ref tv);
            if (fTV.ShowDialog() == DialogResult.OK)
            {
                bindSrcTV.List[bindSrcTV.Position] = tv;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Видалити поточний запис?", "Видалення запису", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                bindSrcTV.RemoveCurrent();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Очистити таблицю?\n\nВсі дані будуть втрачені", "Очищення даних", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                bindSrcTV.Clear();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрити застосунок?", "Вихід з програми", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void gvTV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSaveAsText_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Текстові файли (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.Title = "Зберегти файли в текстовому форматі";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            StreamWriter sw;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8);
                try
                {
                    foreach (TV tv in bindSrcTV.List)
                    {
                        sw.Write(tv.Model + "\t" + tv.Brand + "\t" + tv.ScreenSize + "\t" + tv.Resolution + "\t" + tv.IsSmart + "\t" + tv.Color + "\t" + tv.Price + "\n");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка: \n{0}", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sw.Close();
                }
            }

        }

        private void btnSaveAsBinary_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Файли даних  (*.tv)|*.tv|All files (*.*)|*.*";
            saveFileDialog.Title = "Зберегти дані у бінарному форматі";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            BinaryWriter sw;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                sw = new BinaryWriter(saveFileDialog.OpenFile());
                try
                {
                    foreach (TV tv in bindSrcTV.List)
                    {
                        sw.Write(tv.Brand);
                        sw.Write(tv.Model);
                        sw.Write(tv.ScreenSize);
                        sw.Write(tv.Resolution);
                        sw.Write(tv.IsSmart);
                        sw.Write(tv.Color);
                        sw.Write(tv.Price);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка: \n{0}", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sw.Close();
                }
            }
        }

        private void btnOpenFromText_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстові файли (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.Title = "Прочитати дані у текстовому форматі";
            openFileDialog.InitialDirectory = Application.StartupPath;
            StreamReader sr;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bindSrcTV.Clear(); 
                sr = new StreamReader(openFileDialog.FileName, Encoding.UTF8);
                string s;
                try
                {
                    while ((s = sr.ReadLine())!=null)
                    {
                        string[] split = s.Split('\t');
                        TV tv = new TV(split[0], split[1], int.Parse(split[2]), split[3], bool.Parse(split[4]), split[5], double.Parse(split[6]) );
                        bindSrcTV.Add(tv);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка: \n{0}", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sr.Close();
                }
            }    
        }

        private void btnOpenFromBinary_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файли даних (*.tv)|*.tv|All files (*.*)|*.*";
            openFileDialog.Title = "Прочитати дані у бінарному форматі";
            openFileDialog.InitialDirectory = Application.StartupPath;
            BinaryReader br;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bindSrcTV.Clear();
                br = new BinaryReader(openFileDialog.OpenFile());
                try
                {
                    TV tv; while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        tv = new TV();
                        for (int i = 1; i <= 8; i++)
                        {
                            switch (i)
                            {
                                case 1:
                                    tv.Model = br.ReadString();
                                    break;
                                case 2:
                                    tv.Brand = br.ReadString();
                                    break;
                                case 3:
                                    tv.ScreenSize = br.ReadInt32();
                                    break;
                                case 4:
                                    tv.Resolution = br.ReadString();
                                    break;
                                case 5:
                                    tv.IsSmart = br.ReadBoolean();
                                    break;
                                case 6:
                                    tv.Color = br.ReadString();
                                    break;
                                case 7:
                                    tv.Price = br.ReadDouble();
                                    break;
                            }
                        }
                        bindSrcTV.Add(tv);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка: \n{0}", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    br.Close();
                }
            }
        }
    }
}
