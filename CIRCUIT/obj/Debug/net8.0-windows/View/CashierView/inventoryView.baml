   M S B A M L   `   `   `  ����B  >CIRCUIT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:CIRCUIT.View.CashierViewCIRCUIT.View.CashierView  5   	   W0clr-namespace:CIRCUIT.ViewModel.CashierViewModel"CIRCUIT.ViewModel.CashierViewModel  5
   	   ,    &CIRCUIT.View.CashierView.inventoryView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:CIRCUIT.View.CashierView     5   	   ?vm0clr-namespace:CIRCUIT.ViewModel.CashierViewModel     5
   	   -   5          � WindowStartupLocation$  CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5	   	   
������  � AllowsTransparency$
 True��5   	    � WindowStyle$
 None=�5   	    � Title$ Inventory View��5   	   $	��500��6    $	��850��6-    � DataContext 5      ;   5CIRCUIT.ViewModel.CashierViewModel.InventoryViewModel  5   
   .��5      ��    	palette12#��[  6   $��2q�5       �� CornerRadius$ 10��5      ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 0,1&�63   .��5      � 	 � Color$	 #FF8C00��6   
 � Offset$
 0Q�6/   � 5      $	 #00C8FF��6   $

 0.50Q�6/   � 5      $	 #FF8C00��6   $
 1Q�6/   5      ._�5"   
   � .+�5$      �� $��Product Inventory��6   $��22(�5%      $��SemiBold"�5&      $��Center=�5'      $��	0,10,0,20q�5(        palette6#��[ 5)      ]� 5,      $��0,40,0,0q�6   .+�5-      ��   	palette10#��[ 6   $��8q�5.      $ 10��5/      ._�50      �� -   5        $��SearchTextBox��51      $��35��52      $	��400��53      $��Center=�54      $	��Top=�55      $��10q�56        ModernIconTextBoxStyle#��[ 57      $��10(�58       � ToolTip$# Search products by name or ID��59      $
��Left=�5:      $��	10,0,0,10q�5;      ��50      �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�5;      5<      5=      &   System.Windows.Controls.DataGrid  5?      -   5        $��InventoryDataGrid��5?      $��10,90,10,54q�5@         AutoGenerateColumns$ False��5A         
IsReadOnly$
 True��5B         SelectionMode$ Single=�5C         CanUserAddRows$ False��5D         CanUserDeleteRows$ False��5E         RowHeaderWidth$ 0Q�5F        ModernDataGrid#��[ 5H        StockAlertGridRowStyle   RowStyle# [ 5I        ModernDataGridCell   	CellStyle# [ 5J        ModernDataGridColumnHeader   ColumnHeaderStyle# [ 5K         GridLinesVisibility$
 None=�5L      ��5G      �� *PagedProducts+   Columns 5N      0  *System.Windows.Controls.DataGridTextColumn  5O      ,  &System.Windows.Controls.DataGridColumn   Header$  Product ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6\   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *	ProductId+6\     5P      $ Product Name��6)   $	 320 6_    6?   �� *ProductName+6_     5Q      $ Category��6)   $	 200 6X    6;   �� *
Category+6X     5R      $ Stock Quantity��6)   $	 100 6c    6A   �� *StockQuantity+6c   5T      ]� 5X      $��Bottom=�6   $��10q�63   .+�5Y      �� -   5        $
��Back��5Y      $	��100��5Z      $��35��5[        ExportButtonStyle#��[ 5\      $
��Bold"�5]      $��0q�5^      $
��Left=�5_      $- 'Click to go back to the previous screen��5`      $
��Left=�5a      5b      �� 5d       �� Orientation$ 
Horizontal=�6   $��Center=�5e      .+�5f      �� $�� First��6    	 PaginationButtonStyle#��[	 6.   ��5g      �� *FirstPageCommand+$ PropertyChanged=��� 5i      $�� Prev��6   #��[	 6-   ��5j      �� *PreviousPageCommand+$ PropertyChanged=��� 5l       
 PaginationTextStyle#��[
 5m       ��5l       �� *CurrentPage+$ PropertyChanged=�5m       �� 5o      $��of��6    #��[
 5p       �� 5r      #��[
 5s       ��5r       �� *
TotalPages+$ PropertyChanged=�5s       �� 5u      $��Next >��6   #��[	 6.   ��5v      �� *NextPageCommand+$ PropertyChanged=��� 5x      $��Last >>��6   #��[	 6/   ��5y      �� *LastPageCommand+$ PropertyChanged=�5{      5|      5}      5~      5      