   M S B A M L   `   `   `  ����B  >Opgave1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:Opgave1Opgave1  5   	       Opgave1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Opgave1     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ GUI - Opgave1��5	   	   $	��450��6   $	��800��6,    � 	Resources 5
         TextBoxBackgroundBrush �� Color  ImageBackgroundBrush �� ImageSource j� Stretch  BottonToolbarStyle �� 
TargetType �� Property �� Value	 �� Triggers
 P� Property P� Value3  -System.Windows.Media.Effects.DropShadowEffect   ShadowDepth  LabelToolbarStyle  TextToolbarStyle��%  &	        &	      &	 g     &	 �    &	 �    �� 5   
   $ Black��69   �� 5   
   $ Images/varroamide.jpg��62   $ UniformToFill=�6V   �� 5   
   " ��6+   �5   
   �� 5       � ��$ 32��6&   �� 5       � ��
 ��  �6+   �� 5       � ��$ 	ExtraBold"�6+   �� 5       � ��$ 18(�6)   �� 5       � ��$ Arial)�6+   	 5   
   P� 5      
 �@J�IsMouseOver . . �5      ��  �@J�Effect 5        5      $ 2Q�6+   5      5      5       �� 5#   
   " ��6*   �5$      �� 5%       � ��$ 	ExtraBold"�6/   �� 5&       � ��
 �   �6/   �� 5'       � ��$ 12(�6-   �� 5(       � ��$ Normal$�6.   �� 5)       � ��$ Times New Roman)�6/   5+      �� 5.   
   " ��6)   �5/      �� 50       � ��$ 12(�6-   �� 51       � ��$ Ariel)�6/   �� 52       � ��$ Italic$�6.   	 54      P� 55      
 �@J�	IsFocused . . �56      ��  � ��$ 5q�68   57      59      5:       � DataContext 5;         Opgave1.VarroCounts  5<   
   -   5        varrocounts��5<      .��5>      ]� -   5        $��DockPanelMain��5>      #��[ 6%   .+�5?   
   o� -   5        $��mainMenu��5?   $   $	��Top=�6   .@�5@      m� $��_File��6   .@�5A      m� $
��_New��6    m� Command 6)   �� *NewFileCommand+m� 5B      $��_Open��6    6*   �� *OpenFileCommand+ m� CommandParameter 6N   ��  �� ElementName$ tbxFileName�� �� Path$
 Text�m� 5C      $��_Save��6    6*   �� *SaveCommand+m� 5D      $��Save _As��6    6-   �� *SaveAsCommand+ 6O   �� $ tbxFileName��$
 Text�m� 5E      $��E_xit��6    6*   �� *CloseAppCommand+5F      5G      c� 5H   
   $	��Top=�6   $
��auto��6'   $��20(�5I      $
��Bold"�6    
�������.@�5L      ��  � ToolTip$ Tilføj ny Varroatælling��6   #��[ 5O      ��5N      �� *
AddCommand+.��5P      �� $��Images/Add.png��6   62   5Q      �� 5R      $  Fjern valgt Varroatælling��6   #��[ 5T      ��5S      �� *DeleteCommand+.��5U      �� $��Images/Remove.png��6   65   5V      �� 5W      $��File _Name: ��6   #��[ 5X      $��Center=�5Y       �� Target 6/   �� $ tbxFileName���� 5Z      -   5        $��tbxFileName��5Z      $��Varroamits.dat��5[      #��[ 5\      $��Center=�5]      $	��100��5^      �� 5`      $
��auto��6   $	��5,0q�6%   �� 5b      $&�� _Søg efter Bistade eller dato: ��6   #��[ 5c      $��Center=�5d      �� 5e      -   5        $��tbxSearchBox��5e      $�� ��5f      #��[ 5g      $��Normal$�5h      $��Center=�5i      $	��150��5j      $	��150��5k       �� 	MaxLength$ 18��5l      �� 5m      -   5        $# Søg efter Bistade eller dato��5m       �� 	IsDefault . #��[ 5p      .��5q      �� $��Images/Search.png��6   65   5r      5s      &   System.Windows.Controls.DataGrid  5u   
   -   5        $��dgridVarros��5z         AutoGenerateColumns .  $ �� IsSynchronizedWithCurrentItem$
 TrueY�5w      $��9q�5{       J� Opacity$
 0.66Q�5|         CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .    MinRowHeight$ 15Q�5�      ��5u      ��  �� SelectedIndex 5x      �� $ CurrentIndex� �� SelectedItem 5y      �� $ CurrentVarro�   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn    Header$  Bistade��6%   !   Width5  /System.Windows.Controls.DataGridLengthConverter$! * 66   1  +System.Windows.Controls.DataGridBoundColumn"   Binding" 5�   %   �� $ Bistade�5�        5�      $
  Dato��6%   $! * 63   #   SortDirection$# 
DescendingX�5�   %   " 5�   %   �� $
 Time�$ �� StringFormat$$ 
dd/MM/yyyy��5�        5�      $  Antal Varromider��6%   $! * 6?   " 5�   %   �� $ Count�5�        5�      $  	Kommentar��6%   $! 4* 68   %   CanUserSort% .  " 5�   %   �� $ Comment�5�      5�      5�      5�      