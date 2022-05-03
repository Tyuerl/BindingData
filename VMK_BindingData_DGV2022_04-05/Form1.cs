using System.ComponentModel;
using System.Text;
using System.Text.Json;

namespace VMK_BindingData_DGV2022_04_05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private BindingList<TableRowData> dataList = new();
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dataList;
            try
            {
                dataList.Add(new TableRowData(1, "����", "�����", true, new DateTime(1940, 03, 02), 500000));
                dataList.Add(new TableRowData(2, "����", "���������", true, new DateTime(1940, 03, 02), 500000));
                dataList.Add(new TableRowData(3, "����", "���������", true, new DateTime(1940, 03, 02), 500000));
            }
            catch (BindingException)
            {
                MessageBox.Show("������� ������ ���������");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                var filename = saveFileDialog1.FileName;
                using var file = new FileStream(filename, FileMode.Create);
                using var sw = new StreamWriter(file, Encoding.UTF8);
                var jso = new JsonSerializerOptions();
                jso.WriteIndented = false;
                foreach (var elem in dataList)
                {
                    sw.WriteLine(JsonSerializer.Serialize<TableRowData>(elem, jso));
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                bool t = true;
                var filename = openFileDialog1.FileName;
                using var sr = new StreamReader(filename, Encoding.UTF8);
                dataList.Clear();
                while (!sr.EndOfStream && t)
                {
                    var line = sr.ReadLine() ?? "";
                    try
                    {
                        var obj = JsonSerializer.Deserialize<TableRowData>(line);
                        if (obj is not null)
                        {
                            dataList.Add(obj);
                        }
                    }
                    catch (JsonException)
                    {
                        MessageBox.Show("�� ����������� �� ���� ������� �������");
                        t = false;
                    }
                   
                }
            }
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
                var f2 = new EditForm();
                if (f2.ShowDialog(this) == DialogResult.OK)
                {
                    dataList.Add(f2.UserData);
                }
            }
            catch(BindingException)
            {
                MessageBox.Show("������� ���������� ������\n������ ����� � ������� �� ������ ���� �������");
            }
            //f2.Show(this); //-- �������� ����� � ����������� ������

        }

        private void �������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f2 = new EditForm(dataList[dataGridView1.SelectedRows[0].Index]);
            f2.ShowDialog(this);
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("�� �������, ��� ������ ������� ������ �� ������?", "��������!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataList.RemoveAt(dataGridView1.SelectedRows[0].Index);
                MessageBox.Show("������! ;(", "��! :(", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("�� � ���������! ���� �� ��� ���� ��� ����������!", "(...�����������...)",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                var filename = saveFileDialog1.FileName;
                using var fs = new FileStream(filename, FileMode.Create);
                JsonSerializer.Serialize(fs, dataList);
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }

}