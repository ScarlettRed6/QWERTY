   M S B A M L   `   `   `  ����B  >CIRCUIT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:CIRCUIT.ViewModel.CashierViewModel"CIRCUIT.ViewModel.CashierViewModel  5   	   ,    &CIRCUIT.View.CashierView.voidOrderView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?vm0clr-namespace:CIRCUIT.ViewModel.CashierViewModel     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ 
Void Order��5	   	   $	��500��5
   	   $	��980��5   	   
������  � AllowsTransparency$
 True��5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � DataContext 5      ;   5CIRCUIT.ViewModel.CashierViewModel.VoidOrderViewModel  5   
   .��5      ��    	palette10#��[  6    �� CornerRadius$ 10��5      $��2q�5      ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 0,1&�63   .��5      � 	 � Color$	 #FF8C00��6   
 � Offset$
 0Q�6/   � 5      $	 #00C8FF��6   $

 0.50Q�6/   � 5      $	 #FF8C00��6   $
 1Q�6/   5      ._�5"   
   � $��20q�6    � ColumnDefinitions 5$      �� 5%      $��3* �6#   �� 5&      $��5* �6#   .+�5*      �� $��0��6   $��0,0,20,0q�6)   .+�5,      �� $��
Void Order��6   $��24(�5-      
�������$
��Bold"�5/      $��0,0,0,20q�50      �� 53      $��	Order ID:��6   $��Center=�54      $��0,0,0,10q�55      
�������$��14(�57      �� 58      -   5        $��OrderIdTextBox��58      $	��250��59       � ToolTip$, &Enter the ID of the order to be voided��5;        generalTextBoxStyle#��[ 5<        palette6#��[ 5=      $��0,0,0,20q�5>      ��5:      �� *OrderIdToVoid+ �� UpdateSourceTrigger$ PropertyChanged=�5>      �� 5A      $��Reason for voiding:��6   $��Center=�5B      $��0,0,0,10q�5C      
�������$��14(�5E      �� 5F      -   5        $��ReasonTextBox��5F      $	��250��5G      $	��100��5H       �� TextWrapping$
 Wrap=�5J       �� AcceptsReturn$
 True��5K      #��[ 5L      $- 'Provide a reason for voiding this order��5M      $��0,0,0,20q�5N      #��[ 5O      ��5I      �� *
VoidReason+$ PropertyChanged=�5O      �� 5R       �� Orientation$ 
Horizontal=�6   $��Center=�66   $��0,90,0,0q�6S   .+�5S      �� $��
Void Order��6   $	��120��5T      $��35��5U        	RefundBtn#��[ 5V      $��0q�5W      $( "Confirm voiding the selected order��5Y      $��0,0,10,0q�5Z       J� RenderTransformOrigin$ 0.482,2.539&�6/   ��5X      �� *VoidOrderCommand+5Z   /   �� 5\      -   5        $��Cancel��5\      $	��120��5]      $��35��5^        StartNewSaleButton#��[ 5_      $��0q�5`      $' !Close this window without voiding��5b      5c      5d      �� 5g      $��1��6   .+�5i      �� $��TODAY'S ORDERS��6   $��16(�5j      
�������$
��Bold"�5l      $��0,0,0,10q�5m      &   System.Windows.Controls.DataGrid  5p      -   5        $��OrdersDataGrid��5p         AutoGenerateColumns$ False��5s         CanUserAddRows$ False��5t         
IsReadOnly$
 True��5u        ModernDataGrid#��[ 5v        QuickPayGridRow   RowStyle# [ 5w        ModernDataGridCell   	CellStyle# [ 5x        QuickPayColumnHeader   ColumnHeaderStyle# [ 5y      $��Stretch=�5z      $��Stretch=�5{      $ Select an order to void��5|      $��1q�5}      ��5q      �� *
VoidOrders+ �� SelectedItem 5r      �� *SelectedOrder+ �� Mode$ TwoWay=�   Columns 5~      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Order ID��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6Z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *SaleId+6Z     5�      $ Payment Method��6-   $	 200 6g    6E   �� *PaymentMethod+6g     5�      $ Total Amount��6-   $	 150 6c    6C   �� *TotalAmount+6c     5�      $
 Date��6-    6;   �� *
DateTime+5�      5�      5�      5�      5�      