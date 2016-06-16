﻿using System;
using System.Collections.ObjectModel;
using MaterialForms;

namespace MaterialFormsDemo
{
    public class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var window = new MaterialWindow();
            window.Dialog = new MaterialDialog
            {
                Title = "Demo material forms",
                Form = new MaterialForm
                {
                    new CommandSchema
                    {
                        Name = "Login window",
                        CommandHint = "SHOW",
                        Callback = ShowDemo(LoginDialog)
                    },
                    new CommandSchema
                    {
                        Name = "Dialog inside window",
                        CommandHint = "SHOW",
                        Callback = async arg =>
                        {
                            await window.ShowDialog(new MaterialDialog
                            {
                                Subheading = "Discard draft?",
                                PositiveAction = "DISCARD"
                            }, 250d);
                        }
                    }
                }
            };

            window.Show();
        }

        public static Action<object> ShowDemo(Func<MaterialDialog> dialog)
        {
            Action<object> callback = arg =>
            {
                var window = new MaterialWindow
                {
                    Dialog = dialog()
                };

                window.Show();
            };

            return callback;
        }

        public static MaterialDialog LoginDialog()
        {
            return new MaterialDialog
            {
                Title = "Please log in to continue",
                PositiveAction = "LOG IN",
                Form = new MaterialForm
                {
                    new StringSchema
                    {
                        Key = "User",
                        Name = "Username",
                        IconKind = IconKind.Account
                    },
                    new PasswordSchema
                    {
                        Key = "Pass",
                        Name = "Password",
                        IconKind = IconKind.Key
                    },
                    new BooleanSchema
                    {
                        Key = "Remember",
                        Name = "Remember me",
                        IsCheckBox = true
                    }
                }
            };
        }
    }
}
