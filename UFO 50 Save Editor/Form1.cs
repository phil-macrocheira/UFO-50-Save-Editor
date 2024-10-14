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