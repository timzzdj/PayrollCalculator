using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace PayrollCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btn_CalcPay(object sender, RoutedEventArgs e)
        {
            /**************************************************************************************/
            /* Construct Employee Payroll Information using User Input                            */
            /**************************************************************************************/
            #region
            // Creates a new instance of Employee
            EmployeePay Employee = new EmployeePay();

            // Store User Inputs
            try
            {
                string efn = fname.Text.Trim().Replace(" ", ""); // employee's first name
                string eln = lname.Text.Trim().Replace(" ", ""); // employee's last name
                string eid = empID.Text.Trim().Replace(" ", ""); // employee's ID
                float epr = float.Parse(pRate.Text);             // employee's pay rate
                float ehw = float.Parse(hrsWork.Text);           // employee's work hours


                // Save User Inputs into the Employee instance
                Employee.Employee_fname = efn;
                Employee.Employee_lname = eln;
                Employee.Employee_id = eid;
                Employee.Employee_pRate = epr;
                Employee.Employee_hrs = ehw;
                
                /**************************************************************************************/
                /* Display Employee General Information                                               */
                /**************************************************************************************/
                #region
                    // Checks if Employee Name and ID is valid
                    if (string.IsNullOrWhiteSpace(efn))
                    {
                        MessageBox.Show("First name cannot be empty!", "First name missing!");
                        fname.Text = String.Empty;
                    }
                    else if (string.IsNullOrWhiteSpace(eln))
                    {
                        MessageBox.Show("Last name cannot be empty!", "Last name missing!");
                        lname.Text = String.Empty;
                    }
                    else if (string.IsNullOrWhiteSpace(eid))
                    {
                        MessageBox.Show("Employee ID cannot be empty!", "ID missing!");
                        empID.Text = String.Empty;
                    }
                    else
                    {
                        emp_name.Content = $"{Employee.Employee_fname} {Employee.Employee_lname}";
                        emp_id.Content   = $"{Employee.Employee_id}";
                    }
                    // Checks if Pay Rate is Valid
                    if (epr > EmployeePay.max_pRate)
                    {
                        MessageBox.Show($"Pay rate cannot be more than {EmployeePay.max_pRate}!", "Limit exceeded!");
                        emp_pRate.Content = EmployeePay.max_pRate;
                    }
                    else
                    {
                        emp_pRate.Content = $"{Employee.Employee_pRate.ToString("#.##")}";
                    }
                    #endregion
                /**************************************************************************************/
                /* Calculate and Display Employee Work Hours                                          */
                /**************************************************************************************/
                #region
                    // Checks if Work Hours are valid and Displays Standard Work Hours   
                    if (Employee.Employee_hrs < EmployeePay.min_hours)
                    {
                        MessageBox.Show($"Hours Worked cannot be less than {EmployeePay.min_hours}!", "Insufficient work hours!");
                        hrsWork.Text = EmployeePay.min_hours.ToString();
                    }
                    else if (Employee.Employee_hrs > EmployeePay.max_hours)
                    {
                        MessageBox.Show($"Hours Worked cannot be more than {EmployeePay.max_hours}!", "Limit exceeded!");
                        hrsWork.Text = EmployeePay.max_hours.ToString();
                    }
                    else if (Employee.Employee_hrs <= EmployeePay.min_stdHrs)
                    {
                        emp_std.Content = Employee.Employee_hrs;
                    }
                    else
                    {
                        emp_std.Content = EmployeePay.min_stdHrs;
                    }
                    // Calculate and Display Employee Overtime Work Hours
                    if (Employee.Employee_hrs <= EmployeePay.min_stdHrs)
                    {
                        emp_overtime.Content = 0.0f;
                    }
                    else if (Employee.Employee_hrs > EmployeePay.min_stdHrs)
                    {
                        emp_overtime.Content = (Employee.Employee_hrs - EmployeePay.min_stdHrs).ToString("#.##");
                    }
                    else
                    {
                        emp_overtime.Content = EmployeePay.min_stdHrs - EmployeePay.min_stdHrs;
                    }
                #endregion
                /**************************************************************************************/
                /* Calculate and Display Payroll Information for Standard, Overtime, and Gross Amount */
                /**************************************************************************************/
                #region
                // Calculate and Display Employee Total Standard Pay
                emp_std_result.Content = Employee.StandardTotal().ToString("#.##");

                // Calculate and Display Employee Total Overtime Pay
                emp_overtime_result.Content = Employee.Employee_hrs <= EmployeePay.min_stdHrs ?
                    emp_overtime_result.Content = 0.0f : emp_overtime_result.Content = Employee.OvertimeTotal().ToString("#.##");

                // Calculate and Display Employee Total Gross Pay
                emp_gross_total.Content = Employee.GrossTotal().ToString("#.##");
                #endregion
            }
            catch (FormatException)
            {
                if (String.IsNullOrEmpty(pRate.Text))
                {
                    MessageBox.Show("Please Enter a value!", "Missing Pay Rate!");
                    emp_pRate.Content = EmployeePay.min_pRate.ToString();
                    pRate.Text        = EmployeePay.min_pRate.ToString();
                    btn_CalcPay(sender, e);
                }
                else if (String.IsNullOrEmpty(hrsWork.Text))
                {
                    MessageBox.Show("Please Enter a value!", "Missing Hours Work!");
                    hrsWork.Text = EmployeePay.min_hours.ToString();
                    emp_std.Content = EmployeePay.min_hours.ToString();
                    emp_overtime.Content = EmployeePay.min_hours.ToString();
                    btn_CalcPay(sender, e);
                }
                else
                {
                    MessageBox.Show("Please Enter Valid Values Only!", "Syntax Error!");
                    btn_ClearForm(sender, e);
                }
            }
            #endregion
        }
        private void btn_ClearForm(object sender, RoutedEventArgs e)
        {
            /**************************************************************************************/
            /* Clear Employee Payroll Form                                                        */
            /**************************************************************************************/
            #region
            // Clear User Inputs
            fname.Text   = String.Empty;
            lname.Text   = String.Empty;
            empID.Text   = String.Empty;
            pRate.Text   = String.Empty;
            hrsWork.Text = String.Empty;

            // Clear Employee General Information
            emp_name.Content  = String.Empty;
            emp_id.Content    = String.Empty;
            emp_pRate.Content = String.Empty;

            // Clear Employee Hours Information
            emp_std.Content      = String.Empty;
            emp_overtime.Content = String.Empty;

            // Clear Employee Pay Information
            emp_std_result.Content      = String.Empty;
            emp_overtime_result.Content = String.Empty;
            emp_gross_total.Content     = String.Empty;
            #endregion
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
