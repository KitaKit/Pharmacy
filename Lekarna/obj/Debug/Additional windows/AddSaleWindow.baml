   M S B A M L   `   `   `  ����C  ?Pharmacy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Pharmacy.Additional_windowsPharmacy.Additional_windows  5   	   /    )Pharmacy.Additional_windows.AddSaleWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:Pharmacy.Additional_windows     5   	   -   5          �Name$  addSaleWindow��5   	    � Title$ Add sale��5   	   $	��350��6   $	��650��6'    � WindowStartupLocation$ CenterOwner=�63   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Date:��6-   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dateDatePicker��5      $��0��6-   $��1��6:   $��5q�6J      SelectedDate 5      �� 5       �� Path$
 Date�6    �� UpdateSourceTrigger$ PropertyChanged=�6&   	 �� ValidationRules	 5      5  /System.Windows.Controls.ExceptionValidationRule  5      5      5      �� 5   
   $��1��6   $��0��6   $��Medications:��6-   �� 5    
   -   5        
 ��Name$
 wrapPanelScrollViewer��5        �� PanningMode$
 Both=�66    �� PanningRatio$	 0.2Q�6I   $��Visible=�6\   $��Disabled=�6�   ��. $��1��6�   $��1��6�   .��5!      � -   5         �Name$ medicationsWrapPanel��5!      $��5q�66    � Orientation$ Vertical=�6A   $
��Left=�6X   $��Center=�6s   5"      �� 5$   
   -   5         ��Name$ 
saveButton��5$      $
��Save��6%   $��70��64   $��25��6?   $��3q�6K   $��11��6V   $��1��6d   $��Right=�6t   $��Bottom=�6�   6�   5%      5&      