
// Program 3
// CIS 199-01
// Due: 3/13/17
// By: A5508

// This application calculates the earliest registration date
// and time for an undergraduate student given their credit hours
// and last name.
// Decisions based on UofL Fall/Summer 2017 Priority Registration Schedule

// Solution 3
// This solution keeps the first letter of the last name as a char
// and uses if/else logic for the times.
// It uses defined strings for the dates and times to make it easier
// to maintain.
// It only uses programming elements introduced in the text or
// in class.
// This solution takes advantage of the fact that there really are
// only two different time patterns used. One for juniors and seniors
// and one for sophomores and freshmen. The pattern for sophomores
// and freshmen is complicated by the fact the certain letter ranges
// get one date and other letter ranges get another date.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog2
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        private void findRegTimeBtn_Click(object sender, EventArgs e)
        {
            const string DAY1 = "March 29";  // 1st day of registration
            const string DAY2 = "March 30"; // 2nd day of registration
            const string DAY3 = "March 31"; // 3rd day of registration
            const string DAY4 = "April 3"; // 4th day of registration
            const string DAY5 = "April 4"; // 5th day of registration
            const string DAY6 = "April 5"; // 6th day of registration

            const string TIME1 = "8:30 AM";  // 1st time block
            const string TIME2 = "10:00 AM"; // 2nd time block
            const string TIME3 = "11:30 AM"; // 3rd time block
            const string TIME4 = "2:00 PM";  // 4th time block
            const string TIME5 = "4:00 PM";  // 5th time block

            string[] lowertimes = { TIME5, TIME1, TIME2, TIME3, TIME4, TIME5, TIME1, TIME2, TIME3, TIME4 };
            string[] uppertimes = {TIME3, TIME3, TIME4, TIME4, TIME5, TIME5, TIME1, TIME1, TIME2, TIME2 };
            char[] names = { 'A','C','E','G','J','M','P','R','T','W'};

            string lastNameStr;       // Entered last name
            char lastNameLetterCh;    // First letter of last name, as char
            string dateStr = "Error"; // Holds date of registration
            string timeStr = "Error"; // Holds time of registration
            

            lastNameStr = lastNameTxt.Text; //Allow submission of last name

            //precondition:  Last name is entered into textbox
            //postcondition:  depending on which button is checked, last name is matched up with DAY and TIME strings and 
            // displays registration time
            if (lastNameStr == "") // Empty text box
                MessageBox.Show("Please enter last name!");
            else
            {
                lastNameLetterCh = lastNameStr[0]; //Take first letter of lasst name

                if (!char.IsLetter(lastNameLetterCh)) // Is it a letter or not?
                    MessageBox.Show("Please ensure a letter is in first position of last name!");
                else
                {
                    lastNameLetterCh = char.ToUpper(lastNameLetterCh); // Ensure upper case

                    // Juniors and Seniors share same schedule but different days
                    if (juniorBtn.Checked || seniorBtn.Checked)
                    {
                        if (seniorBtn.Checked)
                            dateStr = DAY1;
                        else // Must be juniors
                            dateStr = DAY2;

                        int sub = names.Length - 1;                         // integer of position is names array
                        while (sub >= 0 && lastNameLetterCh < names[sub])
                            --sub;
                        timeStr = uppertimes[sub];
                       
                    }
                    // Sophomores and Freshmen
                    else // Must be soph/fresh
                    {
                        if (sophBtn.Checked)
                        {
                            // C-O on one day
                            if ((lastNameLetterCh >= 'C') && // >= C and
                                (lastNameLetterCh <= 'O'))   // <= O
                                dateStr = DAY4;
                            else // All other letters on previous day
                                dateStr = DAY3;
                        }
                        else // must be freshman
                        {
                            // C-O on one day
                            if ((lastNameLetterCh >= 'C') && // >= C and
                                (lastNameLetterCh <= 'O'))   // <= O
                                dateStr = DAY6;
                            else // All other letters on previous day
                                dateStr = DAY5;
                        }

                        int sub = names.Length - 1;
                        while (sub >= 0 && lastNameLetterCh < names[sub])
                            --sub;
                        timeStr = lowertimes[sub];
                    }

                    dateTimeLbl.Text = dateStr + " at " + timeStr;
                }
            }
        }
    }
}
