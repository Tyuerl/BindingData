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
                dataList.Add(new TableRowData(1, "????", "?????", true, new DateTime(1940, 03, 02), 500000, 2));
                dataList.Add(new TableRowData(2, "????", "?????????", true, new DateTime(1940, 03, 02), 490000, 3));
                dataList.Add(new TableRowData(3, "????", "?????????", true, new DateTime(1940, 03, 02), 1000000, 0));
            }
            catch (BindingException)
            {
                MessageBox.Show("??????? ?????? ?????????");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void ????????ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f2 = new EditForm();
            if (f2.ShowDialog(this) == DialogResult.OK)
            {
                dataList.Add(f2.UserData);
            }
        //f2.Show(this); //-- ???????? ????? ? ??????????? ??????

        }

        private void ?????????????ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            var f2 = new EditForm(dataList[dataGridView1.SelectedRows[0].Index]);
            f2.ShowDialog(this);
        }

        private void button_infl_Click(object sender, EventArgs e)
        {

        }

        private void ???????ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
            if (dataList.Count == 0 || dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("??????? ??????");
                return;
            }
            if (MessageBox.Show("?? ???????, ??? ?????? ??????? ?????? ?? ???????", "????????!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataList.RemoveAt(dataGridView1.SelectedRows[0].Index);
                MessageBox.Show("??????! ;(", "???! :(", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("?? ? ?????????! ???? ?? ??? ???? ??? ??????????!", "(...???????????...)",
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

        //stat
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void ???????ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TableRowData temp = new TableRowData();
            foreach (TableRowData i in dataList)
            {
                foreach(TableRowData j in dataList)
                {
                    if (j.Sname.CompareTo(i.Sname) > 0)
                    {
                        temp = i.Copy();
                        j.CopyTo(i);
                        temp.CopyTo(j);
                       // MessageBox.Show("asdfasfd");
                    }

                }
            }
        }

        private void ?????ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TableRowData temp = new TableRowData();
            foreach (TableRowData i in dataList)
            {
                foreach (TableRowData j in dataList)
                {
                    if (j.Fname.CompareTo(i.Fname) > 0)
                    {
                        temp = i.Copy();
                        j.CopyTo(i);
                        temp.CopyTo(j);
                        // MessageBox.Show("asdfasfd");
                    }
                }
            }
        }

        private void ????????ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TableRowData temp = new TableRowData();
            foreach (TableRowData i in dataList)
            {
                foreach (TableRowData j in dataList)
                {
                    if (i.Salary > j.Salary)
                    {
                        temp = i.Copy();
                        j.CopyTo(i);
                        temp.CopyTo(j);
                        // MessageBox.Show("asdfasfd");
                    }
                }
            }

        }

        private void ??????ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TableRowData temp = new TableRowData();
            foreach (TableRowData i in dataList)
            {
                foreach (TableRowData j in dataList)
                {
                    if (i.Id < j.Id)
                    {
                        temp = i.Copy();
                        j.CopyTo(i);
                        temp.CopyTo(j);
                        // MessageBox.Show("asdfasfd");
                    }
                }
            }

        }

        private void delete_Click(object sender, EventArgs e)
        {


        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
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
                        MessageBox.Show("?? ??????????? ?? ???? ??????? ???????");
                        t = false;
                    }

                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int sum = 0;
            double sr = 0;
            int count_male = 0;

            foreach (var temp in dataList)
            {
                if (temp.IsMale == true)
                    count_male++;
                sum += (int)temp.Salary;
            }
            sr = ((double)sum) / dataList.Count;
            MessageBox.Show(
                "??????? ?????????? ????????? ?? ?????: " + sum +
                "\n??????? ?????????? ????????? ?? ???????: " + sum * 4 +
                "\n??????? ???????? ?????????v: " + sr +
                "\n??????? ???????? ?????????? ? ????????: " + (count_male % dataList.Count) + "%",
                "??????????", MessageBoxButtons.OK);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void deleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("?? ???????, ??? ?????? ??????? ?????? ?? ???????", "????????!",
             MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                while (dataList.Count > 0)
                    dataList.Remove(dataList[0]);
                MessageBox.Show("??????! ;(", "???! :(", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("?? ? ?????????! ???? ?? ??? ???? ??? ??????????!", "(...???????????...)",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void infl_Click(object sender, EventArgs e)
        {
            Form form3 = new InflForm(dataList);
            form3.ShowDialog();
        }
    }

}