   M S B A M L   `   `   `  ����B  >CIRCUIT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:CIRCUIT.View.CashierViewCIRCUIT.View.CashierView  5   	   1    +CIRCUIT.View.CashierView.reprintreceiptView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:CIRCUIT.View.CashierView     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Reprint Receipt��5	   	   $	��450��6!   $	��800��6.   .��5
      � $��20q�6   .+�5   
   �� $��Reprint Receipt��6   $��18(�5      $
��Bold"�5      $��Center=�5      $��	0,10,0,20q�5      ���#3335      &   System.Windows.Controls.DataGrid  5   
   -   5        $��ReceiptDataGrid��5      $��	10,0,10,0q�5         AutoGenerateColumns$ False��5      $��Stretch=�5      $��Stretch=�5         
IsReadOnly$
 True��5         SelectionMode$ Single=�5         CanUserAddRows$ False��5         CanUserDeleteRows$ False��5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$ Receipt Number��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6_   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6=   �� *ReceiptNumber+6_     5!      $ Customer Name��6%   $	 * 6]   
 6<   �� *CustomerName+6]     5"      $
 Date��6%   $	 * 6L   
 63   �� *Date+6L     5#      $ Amount��6%   $	 * 6P   
 65   �� *Amount+6P   5%      �� 5(   
   -   5        $��Reprint Receipt��5(      $	��120��5)      $��30��5*      $��
0,20,10,10q�5+      $��Right=�5,      $��Bottom=�5-      
������5/      50      51      