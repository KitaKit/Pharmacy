   M S B A M L   `   `   `  ���� C  ?Pharmacy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Pharmacy.Additional_windowsPharmacy.Additional_windows  /    )Pharmacy.Additional_windows.AddSaleWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ;local)clr-namespace:Pharmacy.Additional_windows     -     �Name$  addSaleWindow�� � Title$ Add sale��$	��350��$	��650�� � WindowStartupLocation$ CenterOwner=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��* �.+��� $��0��$��0��$��Date:��(  "System.Windows.Controls.DatePicker  -     Name$ dateDatePicker��$��0��$��1��$��5q�   SelectedDate ��  �� Path$
 Date� �� UpdateSourceTrigger$ PropertyChanged=�	 �� ValidationRules	 5  /System.Windows.Controls.ExceptionValidationRule  �� $��1��$��0��$��Medications:���� -   
 ��Name$
 wrapPanelScrollViewer�� �� PanningMode$
 Both=� �� PanningRatio$	 0.2Q�$��Visible=�$��Disabled=���. $��1��$��1��.��� -    �Name$ medicationsWrapPanel��$��5q� � Orientation$ Vertical=�$
��Left=�$��Center=��� -    ��Name$ 
saveButton��$
��Save��$��70��$��25��$��3q�$��11��$��1��$��Right=�$��Bottom=�