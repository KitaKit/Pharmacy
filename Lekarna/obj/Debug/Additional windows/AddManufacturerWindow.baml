   M S B A M L   `   `   `  ����C  ?Pharmacy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PharmacyPharmacy  5   	   7    1Pharmacy.Additional_windows.AddManufacturerWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Pharmacy     5   	   -   5          �Name$  addManufacturerWindow��5   	    � Title$ Add manufacturer��5   	   $	��250��6"   $	��600��6/    � WindowStartupLocation$ CenterOwner=�6;   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Name:��6-   �� 5   
   -   5         ��Name$ nameTextBox��5      $��0��6'   $��1��64   $��5q�6D   .��6O   ��5      �� 5       �� Path$
 Name�6    �� UpdateSourceTrigger$ PropertyChanged=�6&    �� ValidationRules 5         Pharmacy.NameValidation  5      5      .��5      5      �� 5!   
   $��1��6   $��0��6   $��Country:��6-   �� 5"   
   -   5        $ countryTextBox��5"      $��1��6*   $��1��67   $��5q�6G   .��6R   ��5#      �� 5$      $ Country�6   $ PropertyChanged=�6)    5%          Pharmacy.CountryValidation  5&      5(      .��5)      5*      �� 5,   
   $��2��6   $��0��6   $��License:��6-   �� 5-   
   -   5        $ licenseTextBox��5-      $��2��6*   $��1��67   $��5q�6G   .��6R   ��5.      �� 5/      $ License�6   $ PropertyChanged=�6)    50          Pharmacy.LicenseValidation  51      53      .��54      55      �� 57   
   -   5        	 ��Name$	 
saveButton��57      $
��Save��6%   $��70��64   $��25��6?   $��3q�6K   $��5��6V   $��1��6c   $��Right=�6s   $��Bottom=�6�   6�   58      59      