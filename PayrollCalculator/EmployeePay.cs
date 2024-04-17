using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PayrollCalculator
{
    internal class EmployeePay
    {
        // Public Constants
        public const float min_pRate  = 10.0f;
        public const float min_stdHrs = 40.0f;
        public const float max_pRate = 999.99f;
        public const float min_hours = 0.0f;
        public const float max_hours = 90.0f;
        // Fields
        private string emp_fname;
        private string emp_lname;
        private string emp_id;
        private float  pay_rate;
        private float  hours_work;
        // Default Constructor
        public EmployeePay()
        {
            emp_fname = emp_lname = emp_id = null;
            pay_rate = min_pRate;
            hours_work = 0.0f;
        }
        // Non-default Constructor
        public EmployeePay(string fname, string lname, string id, float hrs = min_hours, float rate = min_pRate)
        {
            emp_fname  = fname;
            emp_lname  = lname;
            emp_id     = id;
            hours_work = hrs;
            pay_rate   = rate;
        }
        // Getters and Setters
        public string Employee_fname 
        { 
            get { return emp_fname; } 
            set 
            {                 
                try
                {
                    emp_fname = value;
                }
                catch (FormatException)
                {
                    if(value == String.Empty)
                    {
                        MessageBox.Show("First name cannot be empty!", "First name missing!");
                    }
                }
            } 
        }
        public string Employee_lname 
        { 
            get { return emp_lname; } 
            set 
            {
                try
                {
                    emp_lname = value;
                }
                catch (FormatException)
                {
                    if (value == String.Empty)
                    {
                        MessageBox.Show("Last name cannot be empty!", "Last name missing!");
                    }
                }
            } 
        }
        public float Employee_pRate 
        { 
            get { return pay_rate; } 
            set 
            {
                try
                {
                    pay_rate = value;
                }
                catch (FormatException)
                {
                    if (value < min_pRate)
                    {
                        MessageBox.Show($"Pay rate cannot be less than {min_pRate}!", "Insufficient pay rate!");
                    }
                    else if(value > max_pRate)
                    {
                        MessageBox.Show($"Pay rate cannot be more than {max_pRate}!", "Limit exceeded!");
                    }
                }
            } 
        }
        public string Employee_id 
        { 
            get { return emp_id; } 
            set 
            {
                try
                {
                    emp_id = value;
                }
                catch (FormatException)
                {
                    if (value.Trim() == String.Empty)
                    {
                        MessageBox.Show("Employee ID cannot be empty!", "ID missing!");
                    }
                }
            } 
        }
        public float Employee_hrs 
        { 
            get { return hours_work; } 
            set 
            {
                try
                {
                    hours_work = value;
                }
                catch (FormatException)
                {
                    if (value <= min_hours)
                    {
                        MessageBox.Show($"Hours Worked cannot be less than {min_hours}!", "Insufficient work hours!");
                    }
                    else if (value > max_hours)
                    {
                        MessageBox.Show($"Hours Worked cannot be more than {max_hours}!", "Limit exceeded!");
                    }
                }
            } 
        }
        // Methods to Calculate Employee Payroll
        public float StandardTotal()
        {
            return hours_work < min_stdHrs ? hours_work * pay_rate : min_stdHrs * Employee_pRate;
        }

        public float OvertimeTotal()
        {
            return hours_work > min_stdHrs ? (hours_work - min_stdHrs) * pay_rate : (min_stdHrs - min_stdHrs) * Employee_pRate;
        }
        public float GrossTotal()
        {
            return StandardTotal() + OvertimeTotal();
        }
    }
}
