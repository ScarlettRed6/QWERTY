   M S B A M L   `   `   `  ����B  >CIRCUIT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:CIRCUIT.View.AdminDashboardCIRCUIT.View.AdminDashboard  5      e7clr-namespace:CIRCUIT.ViewModel.AdminDashboardViewModel)CIRCUIT.ViewModel.AdminDashboardViewModel  5      S.clr-namespace:CIRCUIT.View.AdminDashboardViews CIRCUIT.View.AdminDashboardViews  5      5clr-namespace:CIRCUIT.UtilitiesCIRCUIT.Utilities  5	      -    'CIRCUIT.View.AdminDashboard.CatalogView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:CIRCUIT.View.AdminDashboard     5      Fvm7clr-namespace:CIRCUIT.ViewModel.AdminDashboardViewModel     5      X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  6Q   9view.clr-namespace:CIRCUIT.View.AdminDashboardViews  5      +utilsclr-namespace:CIRCUIT.Utilities  5	      
������   � DataContext  5      @   :CIRCUIT.ViewModel.AdminDashboardViewModel.CatalogViewModel  5   
    � 	Resources 5       �� DataTypeC   =CIRCUIT.ViewModel.AdminDashboardViewModel.AddProductViewModel5   /CIRCUIT.View.AdminDashboardViews.AddProductViewD   >CIRCUIT.ViewModel.AdminDashboardViewModel.EditProductViewModel6   0CIRCUIT.View.AdminDashboardViews.EditProductViewD   >CIRCUIT.ViewModel.AdminDashboardViewModel.ViewProductViewModel6   0CIRCUIT.View.AdminDashboardViews.ViewProductView��%�   (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      5      .��5      ��  �� CornerRadius$ 10��6      	palette12#��[  6   ._�5    
   � .+�5!      �  � RowDefinitions 5"      �� 5#      $��60 �6$   �� 5$      $��60 �6$   �� 5%      $	��70* �6$   �� 5&      $��50 �6$   .+�5*      ��   	palette10#��[ 6   $��3q�5+      $ 5��5,      ._�5.      �  � ColumnDefinitions 5/      �� 50      �� 51      �� 52      $
��auto �6/   .+�57      ��  �� Orientation$ 
Horizontal=�6%   .+�59      !  FontAwesome.Sharp.IconImage  �	  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]] 	  Icon$ Search=�6+     palette6 	  
Foreground# [ 5:   +   $��30��5;   +   $��5q�5<   +   �� 5=      -   5        	 ��Name$	 txtUser��5=   &   $��15(�67   $��Medium"�5>   &   
������   palette1#��[ 5?   &   $��0,0,0,2q�5@   &   $��30��6@   $��Center=�5A   &   $��0,4,0,0q�6A   #��[ 5B   &   
 �� 
CaretBrush#
 [ 5C   &   $	��230��5E   &   ��5D   &   �� *
SearchTerm+ �� UpdateSourceTrigger$ PropertyChanged=�5E   &   5F      �� 5I      $��2��6%   $ 
Horizontal=�5J   %   $��RightToLeft=�5K   %   $��2q�5L   %   .+�5O      �� $	��120��6%   $��40��61   $��0q�5P   %     ExportButtonStyle#��[ 5Q   %   $��Right=�5R   %   ��5S   %   �� *AddNewProduct+.��5T      �� $ 
Horizontal=�6)   .+�5U   "     $
 Plus=�6/   $��15��6;   $��0,0,30,0q�5V   /   �� 5W   "   $	��ADD��6,   $��20(�67   $��Medium"�5X   ,   $��0,0,0,2q�5Y   ,   5Z      5\      �� 5^      $��Archive��6%   $	��120��5_   %   $��40��5`   %   $��20(�5a   %   #��[ 5b   %   ��5c   %   �� *TestCommand+$ PropertyChanged=�5e      5g      5h      �� 5k      #��[ 6   $��1��5l      $��3,0,3,2q�5m      $ 5��5n      ._�5p      �  5q      �� 5r      �� 5s      $
��auto �6/   .+�5w      �� $��1��6%   $ 
Horizontal=�5x   %   $��LeftToRight=�5y   %   .+�5{      �� $��40��6%   $	��120��5|   %   $��10,0,0,0q�5}   %   #��[ 5   %   ��5~   %   �� *ClearFilterFCommand+{�5�   %   �� *	canFilter+.��5�   "   �� $ 
Horizontal=�6-   .+�5�   &     $ X=�63   $��15��6<   $��0,0,10,0q�5�   3   �� 5�   &   $��Clear��60   $��20(�6=   $��Medium"�5�   0   $��0,0,0,2q�5�   0   5�   #   5�      �� 5�      $	��150��6'   $��35��63   $��8,0,0,0q�5�   '   $��15(�5�   '    �� SelectedValuePath$ Content��5�   '     FilterComboBoxStyle#��[ 5�   '    �� SelectedValue 5�   '   �� *FilterPriceRange+$ PropertyChanged=�.@�5�   "   �� $��Price Range��6/    �� 
IsSelected$
 True��6E   �� 5�   "   $��Below ₱100��6/   �� 5�   "   $��₱101 - ₱500��6/   �� 5�   "   $��₱501 - ₱1000��6/   �� 5�   "   $��Above ₱1000��6/   5�      �� 5�      $	��150��6'   $��35��63   $��8,0,0,0q�5�   '   $��15(�5�   '   #��[ 5�   '   ��5�   '   �� *Brands+ 5�   '   �� *FilterBrandBox+$ PropertyChanged=���5�   "   �� 5�   &   .R�5�   *   �� ��64   �� 5�   '   5�      �� 5�      $	��150��6'   $��35��63   $��8,0,2,0q�5�   '   $��15(�5�   '   #��[ 5�   '   ��5�   '   �� *
Categories+ 5�   '   �� *FilterCategoryBox+$ PropertyChanged=���5�   "   �� 5�   &   .R�5�   *   �� ��64   �� 5�   '   5�      5�      5�      5�      &
   System.Windows.Controls.DataGrid
  5�      -   5         
 Name$ ProductDataGrid��5�       
  AutoGenerateColumns$ False��5�       
  CanUserResizeColumns$ False��5�       
  CanUserAddRows$ False��5�      $��2��5�       
  
IsReadOnly$
 True��5�       
  SelectionMode$ Extended=�5�       
  RowHeaderWidth$ 0Q�5�        ModernDataGrid#��[ 5�        ModernDataGridRow 
  RowStyle# [ 5�        ModernDataGridCell 
  	CellStyle# [ 5�       	 ModernDataGridColumnHeader 
  ColumnHeaderStyle# [	 5�      " 
  VerticalScrollBarVisibility$ Disabled=�5�      ��5�      �� *PagedProducts+ 
  Columns 5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 61      HeaderTemplate 5�      �� 5�   "   .R�5�   &   ��  
 ModernCheckBoxStyle#��[
 5�   /   $��5q�5�   /    d� 	IsChecked 5�   /   �� *DataContext.IsAllSelected+ �� RelativeSource ��  �� AncestorType DataGrid  �� Mode$  TwoWay=���5�   /   �� *DataContext.CheckSelectAll+ ��  DataGrid5�   /   5�   #   !   CellTemplate! 5�      �� 5�   "   .R�5�   &   �� #��[
 5�   /   $��5q�5�   /    5�   /   �� *
IsSelected+$ PropertyChanged=�$  TwoWay=���5�   /   �� *DataContext.CheckSelectCell+ ��  DataGrid5�   /   5�   #   5�        5�      "   Header$" Image��61   $	 100 6@   ! 5�      �� 5�   "   .R�5�   &   �� $��80��5�   ,   $��80��5�   ,   $��UniformToFill=�5�   ,   $��Center=�5�   ,   $��Center=�5�   ,   ��5�   ,   �� *	ImagePath+5�   ,   5�   #   5�      0  *System.Windows.Controls.DataGridTextColumn  5�      $" ID��6-   $	 15* 6|   #   DisplayIndex$# 1��6�   1  +System.Windows.Controls.DataGridBoundColumn$   Binding$ 69   �� *	ProductId+$ PropertyChanged=�6�     5�      $
" Name��6-   $	 50* 6�   $ 6;   �� *ProductName+$ PropertyChanged=�6�     5�      $" Brand��6-   $	 30* 6{   $ 6<   �� *Brand+$ PropertyChanged=�6{     5�      $" Stock��6-   $ 70 6�   $ 6<   �� *StockQuantity+$ PropertyChanged=�6�     5�      $
" Cost��6-   $	 25* 6�   $ 6;   �� *
UnitCost+$ PropertyChanged=�% �� StringFormat$% 	₱{0:N2}��6�     5�      $" Price��6-   $	 25* 6�   $ 6<   �� *SellingPrice+$ PropertyChanged=�$% 	₱{0:N2}��6�     5�      $	 50* 61   $" 
Operations��6=   ! 5�      �� 5�   "   .R�5�   &   �� $ 
Horizontal=�61   .+�5�   *   �� $��30��61   $��50��6=   $
��Edit��5�   1     PaginationButtonStyle#��[ 5�   1   ��5�   1   �� *DataContext.EditCommand+ ��  DataGrid��5�   1   �� 5�   1   �� 5�   *   $��30��61   $��50��6=   $
��View��5�   1   #��[ 5�   1   ��5�   1   �� *DataContext.ViewCommand+ ��  DataGrid��5�   1   �� 5�   1   5  '   5  #   5     5     �� 5
     $��3��6   $ 
Horizontal=�6*   $��Center=�6C   $��10q�6`   .+�5     �� $�� First��6   #��[ 6.   ��5     �� *FirstPageCommand+$ PropertyChanged=��� 5     $�� Prev��6   #��[ 6-   ��5     �� *PreviousPageCommand+$ PropertyChanged=��� 5       PaginationTextStyle#��[ 5  !   ��5      �� *CurrentPage+$ PropertyChanged=�5  !   �� 5     $��of��6    #��[ 5      �� 5     #��[ 5      ��5      �� *
TotalPages+$ PropertyChanged=�5      �� 5     $��Next >��6   #��[ 6.   ��5     �� *NextPageCommand+$ PropertyChanged=��� 5     $��Last >>��6   #��[ 6/   ��5     �� *LastPageCommand+$ PropertyChanged=�5      5!     �� 5#     ��6   �� *AddProductView+5$     5&     5(     