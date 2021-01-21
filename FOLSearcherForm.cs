using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FYP_FC_Evaluator___UI
{
	public partial class FOLSearcherForm : Form
	{
		public FOLSearcherForm()
		{
			InitializeComponent();

		}

		private void TopDownSearchButton_Click(object sender, EventArgs e)
		{
			Search(true);
		}

		private void BottomUpSearchButton_Click(object sender, EventArgs e)
		{
			Search(false);
		}

		private void Search(bool isTopDown)
		{
			DateTime start = DateTime.Now;
			resultsGridView.Rows.Clear();
			List<List<Assignment>> res;
			try
			{
			string query = FOLParser.RemoveUnquotedInstancesOf(queryBox.Text, " ");
			res = FOLSearcher.Search(materialBox.Text, query, isTopDown);

				TimeSpan duration = DateTime.Now.Subtract(start);
				MessageBox.Show($"Time taken to evaluate: {duration.Hours} hours, {duration.Minutes} minutes, {duration.Seconds} seconds, {duration.Milliseconds} milliseconds.");
				start = DateTime.Now;

				DisplayResults(res);

				duration = DateTime.Now.Subtract(start);
				MessageBox.Show($"Time taken to display: {duration.Hours} hours, {duration.Minutes} minutes, {duration.Seconds} seconds, {duration.Milliseconds} milliseconds.");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void DisplayResults(List<List<Assignment>> res)
		{
			if(res.Count==0)
			{
				MessageBox.Show("No results found.");
			}
			else if (res[0][0].GetVariableName() == "")
			{
				MessageBox.Show("Tautology.");
			} else if (res.Count == 0)
			{
				MessageBox.Show("Unsatisfiable.");
			}
			else
			{
				for (int i = 0; i < res.Count; i++)
				{
					Console.Out.WriteLine("");

					for (int j = 0; j < res[i].Count; j++)
					{
						resultsGridView.Rows.Add(new string[] { i.ToString(), res[i][j].GetVariableName(), res[i][j].GetValue() });
					}
				}
			}
		}
	}
}
