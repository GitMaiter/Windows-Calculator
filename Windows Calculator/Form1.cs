using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Windows_Calculator
{
    public partial class Form1 : Form
    {
        double myDoubleValue; // Переменная типа Double, для выполнения математических операций.
        string someOperation = ""; // Переменная, для хранения вида математической операции, используется в switch, в методе resultButton_Click();

        bool someOperationPressed = false;
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Please, press \"=\" to confirm your action", "Manual", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void digitButton_Click(object sender, EventArgs e)
        {
            if ((resultBox.Text == "0") || someOperationPressed)  // Если в TextBox записан ноль, или если мы выбрали любую из доступных 
                                                                  // математических операций, значение в TextBox очищается, для записи нового.
            {
                resultBox.Clear();
            }

            someOperationPressed = false;
            Button myButton = (Button)sender;  // Приводип тип объекта sender ко всем кнопкам, которые с обработчиком событий digitButton_Click(), 
                                               // Нужно для того, чтобы не создавать на каждую кнопку отдельный обработчик.

            if (myButton.Text == ",") // Проверка на количество введенных запятых в resultBox.
            {
                if(!resultBox.Text.Contains(","))
                    resultBox.Text = resultBox.Text + myButton.Text;
            }
                
            else
                resultBox.Text = resultBox.Text + myButton.Text;

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            resultBox.Text = "0"; // Метод для очистки resultBox 
        }

        private void actionButton_Click(object sender, EventArgs e)
        {
            Button myActionButton = (Button)sender;
            someOperation = myActionButton.Text;  // Сохраняем знак нашей математической операции.
            myDoubleValue = double.Parse(resultBox.Text);

            memoryLabel.Text = myDoubleValue + " " + someOperation; // Записываем в лейбл предыдущее действие, сверху слева, как в обычном калькуляторе Windows 7.

            someOperationPressed = true; // Необходимо, чтобы удалять предыдущее значение, которое было записано в resultBox, 
                                         // проверка в обработчике  digitButton_Click();
        }

        private void resultButton_Click(object sender, EventArgs e)
        {
            memoryLabel.Text = ""; // После каждой операции, мы очищаем наш лейбл, сверху слева экрана.
            
            switch (someOperation)
            {
                case "-":
                    resultBox.Text = (myDoubleValue - double.Parse(resultBox.Text)).ToString(); 
                    break;

                case "+":
                    resultBox.Text = (myDoubleValue + double.Parse(resultBox.Text)).ToString();
                    break;

                case "*":
                    resultBox.Text = (myDoubleValue * double.Parse(resultBox.Text)).ToString();
                    break;

                case "/":  // Как вариант, можно использовать Try - Catch.
                    
                    if(resultBox.Text != "0") // Проверка деления на 0.
                    {
                        resultBox.Text = (myDoubleValue / double.Parse(resultBox.Text)).ToString();
                    }
                    else
                    {
                        resultBox.Text = "0";
                        MessageBox.Show("You can't divide by zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;

                case "1/x":
                    resultBox.Text = (1 / myDoubleValue).ToString();
                    break;

                case "√":
                    if(resultBox.Text != "0" )
                        resultBox.Text = (Math.Sqrt(myDoubleValue)).ToString();
                    break;

                case "±":
                    if (Convert.ToDouble(resultBox.Text) > 0)
                        resultBox.Text = (myDoubleValue - (myDoubleValue * 2)).ToString();
                    else
                        resultBox.Text = (myDoubleValue).ToString(); 
                    break;

                case "←":      
                    double count = 0;
                    int x = 10;
                    // resultBox.Text = Convert.ToString(myDoubleValue).Remove(Convert.ToString(myDoubleValue).Length - 1); // Короткая версия с методом Remove.
                    for (int i = 0; i <= Convert.ToString(myDoubleValue).Length; i++)  // Моя самодельная версия удаления.
                    {
                        if (x == 0)
                            break;

                        count = myDoubleValue / x % 10;
                        x /= 10;           
                    }
                    
                    myDoubleValue = (myDoubleValue - count) / 10;
                   
                    resultBox.Text = Convert.ToString(myDoubleValue); 
                    break;

                case "%":
                    resultBox.Text = (( myDoubleValue / 100) * double.Parse(resultBox.Text)).ToString();
                    break;

                default:
                    break;
            }

        }

    }
}
