   M S B A M L   `   `   `  ����C  ?Pharmacy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PharmacyPharmacy  5   	   "    Pharmacy.AddMedicationWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Pharmacy     5   	   -   5          �Name$  addMedicationWindow��5   	    � Title$ Add medication��5   	   $	��450��6    $	��800��6-    � 
ResizeMode$ NoResize=�69    � WindowStartupLocation$ CenterOwner=�6O   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��85 �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Title:��6-   �� 5   
   -   5         ��Name$ titleTextBox��5      $��0��6(   $��1��65   $��5q�6E   .��6P   ��5      �� 5        �� Path$ Title�6    �� UpdateSourceTrigger$ PropertyChanged=�6'   	 �� ValidationRules	 5!         Pharmacy.NameValidation  5"      5$      .��5%      5&      �� 5(   
   $��1��6   $��0��6   $��	Category:��6-   �� 5)   
   -   5        
 ��Name$
 categoryComboBox��5)      $��1��6-   $��1��6:   $��5q�6J   �� 5+   
   $��2��6   $��0��6   $��Form:��6-   �� 5,   
   -   5        $
 formComboBox��5,      $��2��6)   $��1��66   $��5q�6F   �� 5.   
   $��3��6   $��0��6   $��Availability:��6-   �� 5/   
   -   5         ��Name$ availabilityCheckBox��5/      $��3��61   $��1��6>   $��5q�6N   �� 51   
   $��4��6   $��0��6   $��Count:��6-   �� 52   
   -   5        $ countTextBox��52      $��4��6(   $��1��65   $��5q�6E   .��6P   ��53      �� 54      $ Count�6   $ PropertyChanged=�6'   	 55      5  /System.Windows.Controls.ExceptionValidationRule  56      58      .��59      5:      �� 5<   
   $��5��6   $��0��6   $��
Warehouse:��6-   �� 5=   
   -   5        $
 warehouseComboBox��5=      $��5��6.   $��1��6;   $��5q�6K   �� 5?   
   $��6��6   $��0��6   $��Prescription:��6-   �� 5@   
   -   5        $ prescriptionCheckBox��5@      $��6��61   $��1��6>   $��5q�6N   �� 5B   
   $��7��6   $��0��6   $��Expiration Date:��6-   (  "System.Windows.Controls.DatePicker  5C   
   -	   5          Name$ expirationDateDatePicker��5C      $��7��67   $��1��6D   $��5q�6T      SelectedDate 5D      �� 5E      $ ExpirationDate�6   $ PropertyChanged=�60   	 5F        5G      5I      5K      �� 5M   
   $��8��6   $��0��6   $��Price:��6-   �� 5N   
   -
   5        $ priceTextBox��5N      $��8��6(   $��1��65   $��5q�6E   .��6P   ��5O      �� 5P      $ Price�6   $ PropertyChanged=�6'   	 5Q        5R      5T      .��5U      5V      �� 5X   
   $��9��6   $��0��6   $��Manufacturer:��6-   �� 5Y   
   -   5        $
 manufacturerComboBox��5Y      $��9��61   $��1��6>   $��5q�6N   �� 5[   
   $��10��6   $��0��6   $��Description:��6.   �� 5\   
   -   5        $ descriptionTextBox��5\      $��10��6.   $��1��6<   $��5q�6L    �� TextWrapping$
 Wrap=�6W    �� AcceptsReturn . 6k   �� 5^   
   -   5         ��Name$ 
saveButton��5^      $
��Save��6%   $��70��64   $��25��6?   $��3q�6K   $��11��6V   $��1��6d   $��Right=�6t   $��Bottom=�6�   6�   5_      5`      