using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PersonalFinancialTracker
{
    public partial class Form1 : Form
    {
        private TextBox txtAmount;
        private TextBox grossAmount;
        private ComboBox cmbType;
        private TextBox txtDescription;
        private ListBox lstEntries;
        private Label lblBalance;
        private Button btnAddEntry;
        // save all entries in the memory i.e. income and expenses
        private List<FinanceEntry> entries = new List<FinanceEntry>();

        public Form1()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Form1";
            this.Text = "Personal Financial Tracker";
            this.ResumeLayout(false);
        }
        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            decimal amount;
            if (decimal.TryParse(txtAmount.Text, out amount))
            {
                string type = cmbType.SelectedItem.ToString();
                string description = txtDescription.Text;

                FinanceEntry entry = new FinanceEntry
                {
                    Amount = amount,
                    Type = type,
                    Description = description,
                    Date = DateTime.Now
                };

                entries.Add(entry);
                UpdateList();
                UpdateBalance();
            }
            else
            {
                MessageBox.Show("Please enter a valid amount.", "Invalid Input", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void UpdateList()
        {
            lstEntries.Items.Clear();
            foreach (var entry in entries)
            {
                lstEntries.Items.Add($"{entry.Date.ToShortDateString()} - {entry.Type}: {entry.Amount:C} - {entry.Description}");
            }
        }

        private void UpdateBalance()
        {
            decimal balance = 0;
            foreach (var entry in entries)
            {
                balance += entry.Type == "Income" ? entry.Amount : -entry.Amount;
            }
            lblBalance.Text = $"Balance: {balance:C}";
        }


    }

    public class FinanceEntry
    {
        public decimal Amount { get; set; }
        public string Type { get; set; } // "Income" or "Expense"
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}