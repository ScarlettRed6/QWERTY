   M S B A M L   `   `   `  ����B  >CIRCUIT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:CIRCUIT.View.CashierViewCIRCUIT.View.CashierView  5   	   5clr-namespace:CIRCUIT.ViewModelCIRCUIT.ViewModel  5   	   +    %CIRCUIT.View.CashierView.quickpayView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:CIRCUIT.View.CashierView     5   	   .vmclr-namespace:CIRCUIT.ViewModel     5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � WindowStyle$
 None=�5
   	   
������  � AllowsTransparency$
 True��5   	    � Title$ Quick Pay View��5   	   $	��400��6    $	��600��6-    � DataContext 5      (   "CIRCUIT.ViewModel.NewSaleViewModel  5   
   .��5      ��    	palette10#��[  6    �� CornerRadius$ 10��5      $��2q�5      ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 0,1&�63   .��5      �  � Color$ #FF8C00��6   	 � Offset$	 0Q�6/   � 5      $ #0000AA��6   $
	 0.50Q�6/   � 5      $ #FF8C00��6   $	 1Q�6/   5      ._�5   
   � $��10q�6   
 � RowDefinitions
 5      �� 5       $
��Auto �6    �� 5!      $
��Auto �6    �� 5"      $
��Auto �6    �� 5#      $
��Auto �6    �� 5$      $��* �6    �� 5%      $
��Auto �6    �� 5&      $
��Auto �6     � ColumnDefinitions 5(      �� 5)      $��* �6#   �� 5*      $
��Auto �6#   .+�5.      �� $��Quick Payment��6   $��18(�5/      $
��Bold"�50      $��Center=�51      $��0��52      $��2��53      $
��0,10q�54        palette6#��[ 55      �� 58      $��	CASH ONLY��6   $
��Bold"�59      $��15(�5:      $��Center=�5;      $��Center=�5<      $��2��5=      $��2��5>      $��10,10q�5?      #��[ 5@      &   System.Windows.Controls.DataGrid  5C      -   5        $��ItemsDataGrid��5C      $��3��5D      $��0��5E      $��2��5F      $��
10,0,10,10q�5G      $	��160��5H         AutoGenerateColumns$ False��5I      $	��Top=�5J         CanUserAddRows$ False��5K         CanUserDeleteRows$ False��5L         
IsReadOnly$
 True��5M         RowHeaderWidth$ 0Q�5N        ModernDataGrid#��[ 5O        QuickPayGridRow   RowStyle# [ 5P        ModernDataGridCell   	CellStyle# [ 5Q        QuickPayColumnHeader   ColumnHeaderStyle# [ 5R      ��5T      �� *
OrderItems+   Columns 5U      0  *System.Windows.Controls.DataGridTextColumn  5V      ,  &System.Windows.Controls.DataGridColumn   Header$ 
 Item Name��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5W   )   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5V   =   �� *ProductName+5W   )     5X      $ Quantity��6)   $ * 6X    6;   �� *
Quantity+6X   5Z      �� 5]      $��4��6   $��1��5^        palette2#��[ 5_        	palette11#��[ 5`      $��1q�5a      $ 5��5b      $��
10,5,10,10q�5c       �� Padding$ 10q�5d      ._�5e      ��  �� Orientation$ 
Horizontal=�6   .+�5f      �� $��Total:��6    $
��Bold"�5g       $��0,0,10,0q�5h       $��Center=�5i       #��[ 5j       �� 5k      -   5        $��TotalAmountTextBlock��5k       $
��Bold"�5m       ���#2FFB2F$��16(�5o       $��Center=�5p       ��5l       �� *TotalAmount+ �� StringFormat$ 	₱{0:N2}��5p       5q      5r      �� 5u      -   5        $��Process Payment��5u      $	��150��5v      $��30��5w      $��
0,20,10,10q�5x      $��Right=�5y      $��Bottom=�5z      $��5��5{      $��1��5|      $��15(�5}        ExportButtonStyle#��[ 5~      ��5      �� *$"ViewModel.ExecuteQuickOrderCommand+5�      �� 5�      -   5        $
��Back��5�      $��30��5�      $��80��5�      $��10,0,320,10q�5�      $��Bottom=�5�      $��5��5�      $��0��5�      #��[ 5�      5�      5�      5�      5�      