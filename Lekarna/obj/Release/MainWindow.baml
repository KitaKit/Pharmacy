   M S B A M L   `   `   `  ���� C  ?Pharmacy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PharmacyPharmacy      Pharmacy.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:Pharmacy     -     �Name$  PharmacyMainWindow�� � Title$ Pharmacy��$	��600��$	��950�� � WindowStartupLocation$ CenterScreen=�$	��400��$	��200��.��� -    �Name$ MainGrid�� � ColumnDefinitions �� .+�o� $��18��$	��Top=�$
��Left=�.@�m� $
��Menu��$��50��.@�m� -    m�Name$ menuItemLoadFromDataBase��$��Load from DataBase��m� -   $ menuItemLoadFromCSVFile��$)��#Load from CSV-file to current table��m� -   $ menuItemSaveToNewCSVFile��$(��"Save current table to new CSV-file��o� $��18��$	��Top=�$��Right=�.@�m� -   $ menuItemSearch��$��Search��$��50��m� -   $ menuItemSort��$
��Sort��$��50���� -    ��Name$ mainTabControl�� �� TabStripPlacement$ Bottom=�$��	0,18,0,30q�.@��� -	    ��Name$ tabItemMedications��$��Medications��.��� ���	#FFE5E5E5.+��� -
   	 ��Name$%	 dataGridMedicationsScrollViewer��
 �� PanningMode$

 Both=� �� PanningRatio$	 0.2Q�.��&   System.Windows.Controls.DataGrid  -     Name$ dataGridMedications��   CanUserReorderColumns .     AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .   � 	Resources  �� 
TargetType   WrapText �� Property �� Value��%/   &	        �� " ��.���  � ��$
 Wrap=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��-   dataGridMedicationsColumnId��   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $ Title��-   "dataGridMedicationsColumnTitle�� ��  �� Path$ Title� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidationRules    Pharmacy.NameValidation    $ Category��-   %!dataGridMedicationsColumnCategory�� �� *
Category+  $
 Form��-   !dataGridMedicationsColumnForm�� �� *Form+  $ Availability��-   )%dataGridMedicationsColumnAvailability�� �� $ Availability�$ PropertyChanged=� 5  /System.Windows.Controls.ExceptionValidationRule    $ Count��-   "dataGridMedicationsColumnCount�� �� $ Count�$ PropertyChanged=�     $ 	Warehouse��-   &"dataGridMedicationsColumnWarehouse�� �� *	Warehouse+  $ Prescription��-   )%dataGridMedicationsColumnPrescription�� �� $ Prescription�$ PropertyChanged=�     $ ExpirationDate��-   +'dataGridMedicationsColumnExpirationDate�� �� $ ExpirationDate�$ PropertyChanged=� �� StringFormat$ 
dd.MM.yyyy��     $ Price��-   "dataGridMedicationsColumnPrice�� �� $ Price�$ PropertyChanged=�$ N2��     $ Manufacturer��-   )%dataGridMedicationsColumnManufacturer�� �� *Manufacturer+  $ Description��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * -   ($dataGridMedicationsColumnDescription��   ElementStyle# [   �� *Description+�� -   $ tabItemWarehouses��$��
Warehouses��.��� ���	#FFE5E5E5.+��� -   $$	 dataGridWarehousesScrollViewer��$

 Both=�$	 0.2Q�.��  -   $ dataGridWarehouses�� .   .   .   ��%/   &	        �� " ��.���  � ��$
 Wrap=�   $ Id�� .  �� *Id+  $
 Name�� �� $
 Name�$ PropertyChanged=�     $ Medications��# [  $ *  .  �� *Medications+�� -   $ tabItemManufacturers��$��Manufacturers��.��� ���	#FFE5E5E5.+��� -   $'	 !dataGridManufacturersScrollViewer��$

 Both=�$	 0.2Q�.��  -   $ dataGridManufacturers�� .   .   .   ��%/   &	        �� " ��.���  � ��$
 Wrap=�   $ Id�� .  �� *Id+  $
 Name�� �� $
 Name�$ PropertyChanged=�     $ Country�� �� $ Country�$ PropertyChanged=�     Pharmacy.CountryValidation    $ License�� �� $ License�$ PropertyChanged=�  	   Pharmacy.LicenseValidation	    $ Medications��# [  $ *  .  �� *Medications+�� -   $ tabItemSales��$��Sales��.��� ���	#FFE5E5E5.+��� -   $	 dataGridSalesScrollViewer��$

 Both=�$	 0.2Q�.��  -    $ dataGridSales�� .   .   .   ��%/   &	        �� " ��.���  � ��$
 Wrap=�   $ Id�� .  �� *Id+  $ Price�� �� $ Price�$ PropertyChanged=�$ N2��     $
 Date�� �� $
 Date�$ PropertyChanged=�$ {0:dd.MM.yyyy}��     $ Medications��# [  $ *  .  �� *Medications+�� -!   $ tabItemPurchases��$��	Purchases��.��� ���	#FFE5E5E5.+��� -"   $#	 dataGridPurchasesScrollViewer��$

 Both=�$	 0.2Q�.��  -#   $ dataGridPurchases�� .   .   .   ��%/   &	        �� " ��.���  � ��$
 Wrap=�   $ Id�� .  �� *Id+  $ DeliveryDate�� �� $ DeliveryDate�$ PropertyChanged=�$ {0:dd.MM.yyyy}��     $
 Cost�� �� $
 Cost�$ PropertyChanged=�$ N2��     $ Provider�� �� *
Provider+  $ Medications��# [  $ *  .  �� *Medications+��  �� Orientation$ 
Horizontal=�$
��Left=�$��Bottom=�$��30��.+��� -$     ��Name$  	addButton��$	��Add��$��70��$��3q��� -%   $  deleteButton��$��Delete��$��70��$��3q��� $ 
Horizontal=�$��Right=�$��Bottom=�$��30��.+��� -&   $  
saveButton��$
��Save��$��70��$��3q��� -'   $  closeButton��$��Close��$��70��$��3q��� -(   ! ��Name$! stackPanelSearch��$��0��$	��250��$��Right=�$	��Top=�$
��0 18q�$y�	Collapsed=�.+��� -)   " ��Name$" textBoxSearch��$��25���� -*   $  buttonClearSearchText��$��Clear��$��Right=�$	��0.5q�$��45��$��24���� -+   $! stackPanelMedicationsSort��$��0��$	��300��$��Right=�$	��Top=�$��0 18 5 0q�
�������$y�	Collapsed=�# � DataContext# 
   Pharmacy.MedicationModel
  .+�� $��0q� �� $
��Auto ��� $��* �$ � RowDefinitions$ �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $
��Form��$��0��$��0���� -,   % ��Name$% comboBoxSortForm��$��0��$��1���� $��Category��$��1��$��0���� --   $% comboBoxSortCategory��$��1��$��1���� $��Availability��$��2��$��0���� -.   $% comboBoxSortAvailability��$��2��$��1��.@��� $
��True���� $��False���� -/   $  buttonSortCount��$��
Count from��$��3��$��0���� -0   $" textBoxSortCount��$��3��$��1��$��1q�.������ $ Count�$ PropertyChanged=�   .���� $��	Warehouse��$��4��$��0���� -1   $% comboBoxSortWarehouse��$��4��$��1���� $��Prescription��$��5��$��0���� -2   $% comboBoxSortPrescription��$��5��$��1��$��Center=�.@��� $
��True���� $��False���� -3   $  buttonSortPrice��$��
Price from��$��6��$��0���� -4   $" textBoxSortPrice��$��6��$��1��.������ $ Price�$ PropertyChanged=�   .���� $��Manufacturer��$��7��$��0���� -5   $% comboBoxSortManufacturer��$��7��$��1���� -6   $  buttonSortApply��$��8��$��0��$��1q�$��Center=�$��Stretch=�$��20��$��Apply��$
��Auto���� -7   $  buttonSortClear��$��8��$��1��$��1q�$��Center=�$
��Left=�$��20��$��Clear��$��60���� -8   $! stackPanelSortWarehouses��$��0��$	��300��$��Right=�$	��Top=�$
��0 18q�
�������$y�	Collapsed=�.+��� -9   $)	 #wrapPanelSortWarehousesScrollViewer��$

 Both=�$	 0.2Q�$
��Auto=�$��Disabled=���. .��� -:   & �Name$& sortPanelWarehouses��' � Orientation$' Vertical=�$
��Left=�$��Center=�$��2��$	��150��� $��Bottom=�$��2q� �� $
��Auto ��� $
��Auto �.+��� -;   $  buttonSortWarehousesApply��$��Apply��$��Bottom=�$��Center=�$��20��$��60��$��0���� -<   $  buttonSortWarehousesClear��$��Clear��$��Bottom=�$��Center=�$��20��$��60��$��1���� -=   $!! stackPanelSortManufacturers��$��0��$	��300��$��Right=�$	��Top=�$��0 18 5 0q�
�������$y�	Collapsed=�.+��  �� $
��Auto ��� $��* �.+��� $��Country��$��0���� ->   $&%  comboBoxSortManufacturersCountry��$��1��$��1q��� -?   $,	 &wrapPanelSortManufacturersScrollViewer��$

 Both=�$	 0.2Q�$
��Auto=�$��Disabled=���. .��� -@   $& sortPanelManufacturers��$' Vertical=�$
��Left=�$��Center=�$��2��$	��150���  �� $
��Auto ��� $
��Auto �.+��� -A   $"  buttonSortManufacturersApply��$��Apply��$��Bottom=�$��Center=�$��20��$��60��$��0���� -B   $"  buttonSortManufacturersClear��$��Clear��$��Bottom=�$��Center=�$��20��$��60��$��1���� -C   $! stackPanelSortSales��$��0��$	��300��$��Right=�$	��Top=�$��0 18 5 0q�
�������$y�	Collapsed=�#    Pharmacy.SaleModel  .+��  �� $
��Auto ��� $��* �.+��� -D   $  buttonSalesSortPrice��$��
Price from��$��0���� -E   $" textBoxSalesSortPrice��$��1��.������ $ Price�$ PropertyChanged=�   .���� -F   $$	 wrapPanelSortSalesScrollViewer��$

 Both=�$	 0.2Q�$
��Auto=�$��Disabled=���. .��� -G   $& sortPanelSales��$' Vertical=�$
��Left=�$��Center=�$��2��$	��150���  �� $
��Auto ��� $
��Auto �.+��� -H   $  buttonSortSalesApply��$��Apply��$��Bottom=�$��Center=�$��20��$��60��$��0���� -I   $  buttonSortSalesClear��$��Clear��$��Bottom=�$��Center=�$��20��$��60��$��1���� -J   $! stackPanelSortPurchases��$��0��$	��300��$��Right=�$	��Top=�$��0 18 5 0q�
�������$y�	Collapsed=�#    Pharmacy.PurchaseModel  .+��  �� $
��Auto ��� $��* �$ �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -K   $  buttonPurchasesSortCost��$��	Cost from��$��0��$��0���� -L   $" textBoxPurchasesSortCost��$��1��$��0��.������ $
 Cost�$ PropertyChanged=�   .���� $��Country��$��0��$��1���� -M   $#% comboBoxSortPurchasesProvider��$��1��$��1q�$��1���� -N   $(	 "wrapPanelSortPurchasesScrollViewer��$

 Both=�$	 0.2Q�$
��Auto=�$��Disabled=���. .��� -O   $& sortPanelPurchases��$' Vertical=�$
��Left=�$��Center=�$��2��$	��150���  �� $
��Auto ��� $
��Auto �.+��� -P   $  buttonSortPurchasesApply��$��Apply��$��Bottom=�$��Center=�$��20��$��60��$��0���� -Q   $  buttonSortPurchasesClear��$��Clear��$��Bottom=�$��Center=�$��20��$��60��$��1��