   M S B A M L   `   `   `  ���� C  ?Pharmacy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PharmacyPharmacy  "    Pharmacy.AddMedicationWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:Pharmacy     -     �Name$  addMedicationWindow�� � Title$ Add medication��$	��450��$	��800�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterOwner=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��85 ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��* �.+��� $��0��$��0��$��Title:���� -    ��Name$ titleTextBox��$��0��$��1��$��5q�.������  �� Path$ Title� �� UpdateSourceTrigger$ PropertyChanged=�	 �� ValidationRules	    Pharmacy.NameValidation  .���� $��1��$��0��$��	Category:���� -   
 ��Name$
 categoryComboBox��$��1��$��1��$��5q��� $��2��$��0��$��Form:���� -   $
 formComboBox��$��2��$��1��$��5q��� $��3��$��0��$��Availability:���� -    ��Name$ availabilityCheckBox��$��3��$��1��$��5q��� $��4��$��0��$��Count:���� -   $ countTextBox��$��4��$��1��$��5q�.������ $ Count�$ PropertyChanged=�	 5  /System.Windows.Controls.ExceptionValidationRule  .���� $��5��$��0��$��
Warehouse:���� -   $
 warehouseComboBox��$��5��$��1��$��5q��� $��6��$��0��$��Prescription:���� -   $ prescriptionCheckBox��$��6��$��1��$��5q��� $��7��$��0��$��Expiration Date:��(  "System.Windows.Controls.DatePicker  -	     Name$ expirationDateDatePicker��$��7��$��1��$��5q�   SelectedDate �� $ ExpirationDate�$ PropertyChanged=�	   �� $��8��$��0��$��Price:���� -
   $ priceTextBox��$��8��$��1��$��5q�.������ $ Price�$ PropertyChanged=�	   .���� $��9��$��0��$��Manufacturer:���� -   $
 manufacturerComboBox��$��9��$��1��$��5q��� $��10��$��0��$��Description:���� -   $ descriptionTextBox��$��10��$��1��$��5q� �� TextWrapping$
 Wrap=� �� AcceptsReturn . �� -    ��Name$ 
saveButton��$
��Save��$��70��$��25��$��3q�$��11��$��1��$��Right=�$��Bottom=�