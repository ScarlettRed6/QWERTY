   M S B A M L   `   `   `  ����B  >CIRCUIT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulle7clr-namespace:CIRCUIT.ViewModel.AdminDashboardViewModel)CIRCUIT.ViewModel.AdminDashboardViewModel  5      S.clr-namespace:CIRCUIT.View.AdminDashboardViews CIRCUIT.View.AdminDashboardViews  5      S.clr-namespace:CIRCUIT.View.AdminDashboardViews CIRCUIT.View.AdminDashboardViews  5      0    *CIRCUIT.View.AdminDashboardViews.StaffView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Fvm7clr-namespace:CIRCUIT.ViewModel.AdminDashboardViewModel     5      ?view.clr-namespace:CIRCUIT.View.AdminDashboardViews     5      @local.clr-namespace:CIRCUIT.View.AdminDashboardViews     5      X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  6K     � DataContext  5      >   8CIRCUIT.ViewModel.AdminDashboardViewModel.StaffViewModel  5   
    � 	Resources 5       �� DataType@   :CIRCUIT.ViewModel.AdminDashboardViewModel.AddUserViewModel5   /CIRCUIT.View.AdminDashboardViews.AddNewUserViewD   >CIRCUIT.ViewModel.AdminDashboardViewModel.EditAccountViewModel6   0CIRCUIT.View.AdminDashboardViews.EditAccountViewD   >CIRCUIT.ViewModel.AdminDashboardViewModel.ViewAccountViewModel6   0CIRCUIT.View.AdminDashboardViews.ViewAccountView��%�   (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      5      .��5      ��  �� CornerRadius$ 10��6      	palette12#��[  6   ._�5   
   � .+�5      �  � RowDefinitions 5       �� 5!      $��65 �6$   �� 5"      $��65 �6$   �� 5#      $��* �6$   �� 5$      $��50 �6$   .+�5(      ��   	palette10#��[ 6   $ 5��5)      $��5,3,5,3q�5*      ._�5+      �  � ColumnDefinitions 5,      �� 5-      �� 5.      �� 5/      $
��auto �6/   .+�53      ��  �� Orientation$ 
Horizontal=�6%   .+�55      �� $��	All Users��6(   $��10,0,0,0q�56   (   $��25(�57   (   $��Medium"�58   (     panelColor4#��[ 59   (   $��Center=�5:   (   �� 5<      $��10,3,0,0q�5=   (   $��25(�5>   (   $��Medium"�5?   (   #��[ 5@   (   $��Center=�5A   (   ��5<   (   �� *
TotalUsers+5A   (   5C      �� 5F      $ 
Horizontal=�6%   $��1��6>   $��Right=�5G   %   .+�5H      !  FontAwesome.Sharp.IconImage  �	  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]] 	  Icon$ Search=�6+     palette6 	  
Foreground# [ 5I   +   $��30��5J   +   $��5q�5K   +   �� 5L      -   5        	 ��Name$	 txtUser��5L   &   $��15(�67   $��Medium"�5M   &   
������ 
 �� 
CaretBrush

 �����
�������$��0,0,0,2q�5O   &   $��30��6@   $��Center=�5P   &   $��5,4,13,0q�6A     panelColor5#��[ 5Q   &   $	��230��5S   &   ��5R   &   �� *
SearchTerm+ �� UpdateSourceTrigger$ PropertyChanged=�5S   &   5T      �� 5W      $	��160��6!   $��40��6-   $��0q�5X   !   $��2��65     ExportButtonStyle#��[ 5Y   !   $��Right=�5Z   !   $��0,0,5,0q�5[   !   ��5\   !   �� *
AddNewUser+.��5]      �� $ 
Horizontal=�6)   .+�5^   "     $
 Plus=�6/   $��15��6;   $��0,0,30,0q�5_   /   �� 5`   "   $��ADD USER��6,   $��15(�6<   $��Medium"�5a   (   $��0,0,0,2q�5b   (   5c      5e      5g      5h      �� 5k      $��1��6   #��[ 5l      $��5,0,5,3q�5m      $ 5��5n      ._�5p      �� $ 
Horizontal=�6!   $��RightToLeft=�5q   !   $��2q�5r   !   .+�5s      �� $��
Deactivate��6!   $	��120��5t   !   $��40��5u   !   $��20(�5v   !   #��[ 5w   !   ��5x   !   �� *DeactivateCommand+$ PropertyChanged=��� 5z      $��Activate��6!   $	��110��5{   !   $��40��5|   !   $��20(�5}   !   #��[ 5~   !   ��5   !   �� *ActivateCommand+$ PropertyChanged=�5�      5�      &
   System.Windows.Controls.DataGrid
  5�      -   5         
 Name$ ProductDataGrid��5�       
  AutoGenerateColumns$ False��5�       
  CanUserAddRows$ False��5�      $��2��5�       
  
IsReadOnly$
 True��5�       
  SelectionMode$ Extended=�5�       
  RowHeaderWidth$ 0Q�5�        ModernDataGrid#��[ 5�        ModernDataGridRow 
  RowStyle# [ 5�        ModernDataGridCell 
  	CellStyle# [ 5�       	 ModernDataGridColumnHeader 
  ColumnHeaderStyle# [	 5�      $��5,0,5,3q�5�      ��5�      �� *
PagedUsers+ 
  Columns 5�      4  .System.Windows.Controls.DataGridTemplateColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 3* 61      HeaderTemplate 5�      �� 5�   "   .R�5�   &   ��  
 ModernCheckBoxStyle#��[
 5�   /   $��10q�5�   /    d� 	IsChecked 5�   /   �� *DataContext.IsAllSelected+ �� RelativeSource ��  �� AncestorType DataGrid �� Mode$ TwoWay=���5�   /   �� *DataContext.CheckSelectAll+ ��  DataGrid5�   /   5�   #      CellTemplate 5�      �� 5�   "   .R�5�   &   �� #��[
 5�   /   $��10q�5�   /    5�   /   �� *
IsSelected+$ PropertyChanged=�$ TwoWay=���5�   /   �� *DataContext.CheckSelectCell+ ��  DataGrid5�   /   5�   #   5�        5�         Header$ Image��61   $	 100 6@    5�      �� 5�   "   .R�5�   &   �� $��80��5�   ,   $��80��5�   ,   $��Uniform=�5�   ,   ��5�   ,   �� *UserImagePath+5�   ,   5�   #   5�      0  *System.Windows.Controls.DataGridTextColumn  5�      $ ID��6-   $ 3* 5�   -   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5�   -   �� *UserId+$ PropertyChanged=�5�   -     5�      $ 	Full Name��6-   $	 12* 5�   -    5�   -   �� *
FullName+$ PropertyChanged=�5�   -     5�      $ Account Name��6-   $	 10* 5�   -    5�   -   �� *
Username+$ PropertyChanged=�5�   -     5�      $
 Role��6-   $ 5* 5�   -    5�   -   �� *Role+$ PropertyChanged=�5�   -     5�      $ Status��6-   $ 5* 5�   -     ModernCellStatusStyle   	CellStyle# [ 5�   -    5�   -   �� *
UserStatus+$ PropertyChanged=�5�   -     5�      $	 10* 61   $ 
Operations��6=    5�      �� 5�   "   .R�5�   &   �� $ 
Horizontal=�61   .+�5�   *   �� $��35��61   $��50��6=   $
��Edit��5�   1   $��2q�5�   1     PaginationButtonStyle#��[ 5�   1   ��5�   1   �� *DataContext.EditCommand+ ��  DataGrid��5�   1   �� 5�   1   �� 5�   *   $��35��61   $��60��6=   $
��View��5�   1   $��2q�5�   1   #��[ 5�   1   ��5�   1   �� *DataContext.ViewCommand+ ��  DataGrid��5�   1   �� 5�   1   5�   '   5�   #   5�      5�      �� 5�      $��3��6   $ 
Horizontal=�5�      $��Center=�5�      $��10q�5�      .+�5�      �� $�� First��6   #��[ 6.   ��5�      �� *FirstPageCommand+$ PropertyChanged=��� 5�      $�� Prev��6   #��[ 6-   ��5�      �� *PreviousPageCommand+$ PropertyChanged=��� 5�        PaginationTextStyle#��[ 5�       ��5�       �� *CurrentPage+$ PropertyChanged=�5�       �� 5�      $��of��6    #��[ 5�       �� 5�      #��[ 5�       ��5�       �� *
TotalPages+$ PropertyChanged=�5�       �� 5�      $��Next >��6   #��[ 6.   ��5�      �� *NextPageCommand+$ PropertyChanged=��� 5�      $��Last >>��6   #��[ 6/   ��5�      �� *LastPageCommand+$ PropertyChanged=�5�      5�      �� 5�      ��6   �� *CurrentView+5�      5     5     