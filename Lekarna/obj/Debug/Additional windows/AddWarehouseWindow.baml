   M S B A M L   `   `   `  ����C  ?Pharmacy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PharmacyPharmacy  5   	   4    .Pharmacy.Additional_windows.AddWarehouseWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Pharmacy     5   	   -   5          �Name$  addWarehouseWindow��5   	    � Title$ Add warehouse��5   	   $	��150��6   $	��400��6,    � WindowStartupLocation$ CenterOwner=�68   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5       � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Name:��6-   �� 5   
   -   5         ��Name$ nameTextBox��5      $��0��6'   $��1��64   $��5q�6D   .��6O   ��5      �� 5       �� Path$
 Name�6    �� UpdateSourceTrigger$ PropertyChanged=�6&    �� ValidationRules 5         Pharmacy.NameValidation  5      5      .��5      5      �� 5   
   -   5        	 ��Name$	 
saveButton��5      $
��Save��6%   $��70��64   $��25��6?   $��3q�6K   $��11��6V   $��1��6d   $��Right=�6t   $��Bottom=�6�   6�   5      5       