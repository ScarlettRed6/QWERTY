   M S B A M L   `   `   `  ����B  >CIRCUIT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulle7clr-namespace:CIRCUIT.ViewModel.AdminDashboardViewModel)CIRCUIT.ViewModel.AdminDashboardViewModel  5      S.clr-namespace:CIRCUIT.View.AdminDashboardViews CIRCUIT.View.AdminDashboardViews  5      S.clr-namespace:CIRCUIT.View.AdminDashboardViews CIRCUIT.View.AdminDashboardViews  5      3    -CIRCUIT.View.AdminDashboardViews.ViewSaleView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Fvm7clr-namespace:CIRCUIT.ViewModel.AdminDashboardViewModel     5      ?view.clr-namespace:CIRCUIT.View.AdminDashboardViews     5      @local.clr-namespace:CIRCUIT.View.AdminDashboardViews     5      X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  6K     � 	Resources  5       �� DataTypeJ   DCIRCUIT.ViewModel.AdminDashboardViewModel.SalesTransactionsViewModel<   6CIRCUIT.View.AdminDashboardViews.SalesTransactionsView��%G   (��       5      *, +)�� 6
   "  6   .R�5        5      5      .��5      ��    	palette12#��[  6    �� CornerRadius$ 10��5      ._�5   
   � .+�5      � $��10q�6    � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $��* �6$   �� 5      $��45 �6$   .+�5       �  5!      �� 5"      �� 5#      .+�5'      ��   	returnBtn#��[ 6   $��5,5,0,0q�5(      $��35��5)      $��35��6)   $
��Left=�5+      ��5*      �� *ReturnBtnCommand+.��5,      !  FontAwesome.Sharp.IconImage    
returnIcon#��[ 5-   '   $��35��5.   '   5/      �� 52      $��1��6   $��Transaction Details��6,     	palette10#��[ 53        palette6#��[ 54      $��15(�55      .��56      � $��10q�6    � ColumnDefinitions 57      �� 58   "   $
��Auto �63   �� 59   "   $��* �63   �� 5:   "   $
��Auto �63   �� 5;   "   $��* �63    5=      �� 5>   "   $
��Auto �60   �� 5?   "   $
��Auto �60   .+�5C      �� $��0��6)   $��0��69   $��0,0,20,0q�6F   .+�5D   "   �� $��	Sales ID:��6,   $
��Bold"�6=   �� 5E   "   $��0,0,0,10q�6H   ��6,   �� *
SalesIdBox+6H   �� 5G   "   $��Cashier ID:��6,   $
��Bold"�6?   �� 5H   "   $��0,0,0,10q�6J   ��6,   �� *CashierIdBox+6J   �� 5J   "   $��Cashier Name:��6,   $
��Bold"�6A   �� 5K   "   ��6,   �� *CashierNameBox+5L      �� 5O      $��2��6)   $��0��69   $��0,0,20,0q�6F   .+�5P   "   �� $��Total Amount:��6,   $
��Bold"�6A   �� 5Q   "   $��0,0,0,10q�6b   ��6,   �� *TotalAmountBox+ �� StringFormat$ 	₱{0:N2}��6b   �� 5S   "   $��
Date/Time:��6,   $
��Bold"�6>   �� 5T   "   $��0,0,0,10q�6I   ��6,   �� *DateTimeBox+6I   �� 5V   "   $��Payment Method:��6,   $
��Bold"�6C   �� 5W   "   $��0,0,0,10q�6N   ��6,   �� *PaymentMethodBox+6N   5X      �� 5[      $��3��6)   $��0��69   .+�5\   "   �� $��Customer Payment:��6,   $
��Bold"�6E   �� 5]   "   $��0,0,0,10q�6P   ��6,   �� *CustomerPaymentBox+6P   �� 5_   "   $��Change Given:��6,   $
��Bold"�6A   �� 5`   "   ��6,   �� *ChangeGivenBox+$ 	₱{0:N2}��5a      5b      5c      5e      �� 5g      $��1��6   #��[ 5h      $��0,3,0,0q�5i      $ 5��5j      ._�5l      �� $��Sales Items��6    $��16(�5m       $
��Bold"�5n       $��5q�5o       #��[ 5p       5q      �� 5t      $��2��6   $ 10��5u      ._�5w      &   System.Windows.Controls.DataGrid  -   5          Name$ ProductDataGrid��5w         AutoGenerateColumns$ False��5x         CanUserResizeColumns$ False��5y      	   CanUserAddRows$	 False��5z      $��2��5{      
   
IsReadOnly$

 True��5|         SelectionMode$ Extended=�5~         RowHeaderWidth$ 0Q�5        ModernDataGrid#��[ 5�        ModernDataGridCell   	CellStyle# [ 5�        ModernDataGridRow   RowStyle# [ 5�        ModernDataGridColumnHeader   ColumnHeaderStyle# [ 5�      ��5}      �� *
PagedSales+   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$	  ID��61      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *
SaleItemId+ �� UpdateSourceTrigger$ PropertyChanged=�6�     5�      $  Product Id��61   $	 140 6�    6F   �� *	ProductId+$ PropertyChanged=�6�     5�      $  Product Name��61   $	 140 6�    6H   �� *ProductName+$ PropertyChanged=�6�     5�      $ 	 Quantity��61   $	 140 6�    6D   �� *
Quantity+$ PropertyChanged=�6�     5�      $  Item Total Price��61   $	 140 6�    6L   �� *ItemTotalPrice+$ PropertyChanged=�$ 	₱{0:N2}��6�   5�      5�      �� 5�      $��3��6   $ 	0,0,10,10��5�      #��[ 5�      ._�5�      �� $��2��6!    �� Orientation$ 
Horizontal=�6.   $��Center=�6G   $��5q�6d    	 PaginationStackPanelStyle#��[	 5�   !   .+�5�      �� $�� First��6!    
 PaginationButtonStyle#��[
 62   ��5�   !   �� *FirstPageCommand+$ PropertyChanged=��� 5�      $�� Prev��6!   #��[
 61   ��5�   !   �� *PreviousPageCommand+$ PropertyChanged=��� 5�        PaginationTextStyle#��[ 5�   $   ��5�   $   �� *CurrentPage+$ PropertyChanged=�5�   $   �� 5�      $��of��6$   #��[ 5�   $   �� 5�      #��[ 5�   $   ��5�   $   �� *
TotalPages+$ PropertyChanged=�5�   $   �� 5�      $��Next >��6!   #��[
 62   ��5�   !   �� *NextPageCommand+$ PropertyChanged=��� 5�      $��Last >>��6!   #��[
 63   ��5�   !   �� *LastPageCommand+$ PropertyChanged=�5�      5�      5�      �� 5�      ��6   �� *CurrentView+5�      5�      5�      