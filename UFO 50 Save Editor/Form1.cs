using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.Json;

namespace UFO_50_Save_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Name = "DeleteCol";
            deleteColumn.HeaderText = "";
            deleteColumn.Text = "✕";
            deleteColumn.UseColumnTextForButtonValue = true;
            deleteColumn.Width = 50;
            deleteColumn.DisplayIndex = 0;
            dataGridView1.Columns.Add(deleteColumn);
            dataGridView1.CellClick += DataGridView1_CellClick;

            VariableCol.Name = "VariableCol";
            ValueCol.Name = "ValueCol";

            string saveFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ufo50", "save1.ufo");
            if (!File.Exists(saveFilePath)) {
                OpenSaveFileDialog();
            }
            else {
                ReadSaveFile(saveFilePath);
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn) {
                if (MessageBox.Show(this, "Delete Variable?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            OpenSaveFileDialog();
        }
        private void BtnOverwrite_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to overwrite your UFO 50 save file?",
                "Confirm Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes) {
                string saveFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ufo50", "save1.ufo");
                CreateBackup(saveFilePath);

                var variables = new Dictionary<string, object>();

                foreach (DataGridViewRow row in dataGridView1.Rows) {
                    if (row.Cells["VariableCol"].Value != null) {
                        string variable = row.Cells["VariableCol"].Value.ToString();
                        string value = row.Cells["ValueCol"].Value?.ToString() ?? "";
                        variables[variable] = value;
                    }
                }

                string jsonString = JsonSerializer.Serialize(variables);
                byte[] bytesToEncode = Encoding.UTF8.GetBytes(jsonString);
                string encodedText = Convert.ToBase64String(bytesToEncode);

                File.WriteAllText(saveFilePath, encodedText);
                MessageBox.Show("UFO 50 save file overwritten. Backup created in 'BACKUPS' folder.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (Form prompt = new Form()) {
                prompt.Width = 400;
                prompt.Height = 120;
                prompt.Text = "New Variable Name";
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.StartPosition = FormStartPosition.CenterParent;

                TextBox inputBox = new TextBox() { Left = 20, Top = 20, Width = 250 };
                Button confirmButton = new Button() { Text = "OK", Left = 290, Top = 19, Width = 70, Height = 32, DialogResult = DialogResult.OK };

                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmButton);
                prompt.AcceptButton = confirmButton;

                if (prompt.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(inputBox.Text)) {
                    string newVariable = inputBox.Text.Trim();

                    foreach (DataGridViewRow row in dataGridView1.Rows) {
                        if (row.Cells["VariableCol"].Value?.ToString().Equals(newVariable, StringComparison.OrdinalIgnoreCase) == true) {
                            MessageBox.Show("A variable with this name already exists.", "Duplicate Variable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    dataGridView1.Rows.Add(inputBox.Text, "");
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1];
                    dataGridView1.BeginEdit(true);
                }
            }
        }
        private void BtnGift_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 50; i++) {
                bool skip = false;
                string giftVariable = "game0_gardenWin" + i;
                foreach (DataGridViewRow row in dataGridView1.Rows) {
                    if (row.Cells["VariableCol"].Value?.ToString().Equals(giftVariable, StringComparison.OrdinalIgnoreCase) == true) {
                        skip = true;
                        break;
                    }
                }
                if (!skip) {
                    dataGridView1.Rows.Add(giftVariable, "1.0");
                }
            }
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1];
        }
        private int lastMatchedIndex = -1;
        private void filterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                string filterText = filterTextBox.Text.ToLower();
                dataGridView1.ClearSelection();

                for (int i = (lastMatchedIndex + 1) % dataGridView1.Rows.Count; i < dataGridView1.Rows.Count; i++) {
                    var cellValue = dataGridView1.Rows[i].Cells["VariableCol"].Value?.ToString().ToLower();
                    if (cellValue != null && cellValue.Contains(filterText)) {
                        dataGridView1.FirstDisplayedScrollingRowIndex = i;
                        dataGridView1.Rows[i].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0]; // Set current cell to the first cell of the found row

                        lastMatchedIndex = i;
                        return;
                    }
                }

                // If no match is found after the last matched index, wrap around and search from the beginning
                for (int i = 0; i <= lastMatchedIndex; i++) {
                    var cellValue = dataGridView1.Rows[i].Cells["VariableCol"].Value?.ToString().ToLower();
                    if (cellValue != null && cellValue.Contains(filterText)) {
                        dataGridView1.FirstDisplayedScrollingRowIndex = i;
                        dataGridView1.Rows[i].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0]; // Set current cell to the first cell of the found row

                        lastMatchedIndex = i;
                        return;
                    }
                }
            }
        }

        private void OpenSaveFileDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ufo50");
                openFileDialog.Filter = "UFO Save Files (*.ufo)|*.ufo|All Files (*.*)|*.*";
                openFileDialog.Title = "Select a UFO Save File";

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    string filePath = openFileDialog.FileName;
                    ReadSaveFile(filePath);
                }
            }
        }

        private void ReadSaveFile(string filePath)
        {
            label1.Text = filePath;
            string rawSave = File.ReadAllText(filePath);
            rawSave = rawSave.TrimEnd('\0');
            byte[] bytes = Convert.FromBase64String(rawSave);
            string decodedString = Encoding.UTF8.GetString(bytes);

            dataGridView1.Rows.Clear();
            var variables = JsonSerializer.Deserialize<Dictionary<string, object>>(decodedString);

            if (variables != null) {
                foreach (var kvp in variables) {
                    dataGridView1.Rows.Add(kvp.Key, kvp.Value?.ToString() ?? "");
                }
            }
        }
        private void CreateBackup(string originalFilePath)
        {
            string backupDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BACKUPS");

            if (!Directory.Exists(backupDirectory)) {
                Directory.CreateDirectory(backupDirectory);
            }

            string backupFileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".ufo";
            string backupFilePath = Path.Combine(backupDirectory, backupFileName);

            File.Copy(originalFilePath, backupFilePath, overwrite: true);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}