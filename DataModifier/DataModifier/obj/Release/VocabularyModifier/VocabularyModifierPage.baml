   M S B A M L   `   `   `  ���� G  CDataModifier, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:DataModifier.VocabularyModifierDataModifier.VocabularyModifier  +clr-namespace:DataModifierDataModifier  <    6DataModifier.VocabularyModifier.VocabularyModifierPage   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ?local-clr-namespace:DataModifier.VocabularyModifier     )dmclr-namespace:DataModifier     -     N�Name$  PageBase�� N� Title$, &Englearner 词库系统 - 单词词库��.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ���  � ColumnDefinitions �� $
��167* ��� $
��633* �.+�]� $��2��.+�o� $	��Top=�.@�m� $��文件��.@�m� -    m�Name$ openFile��$��打开��   OpenCommand m� Command# �   m� -   $ saveFile��$��保存��  SaveCommand# �  m� -   $ saveAs��$��	另存为��  SaveAsCommand# �  m� -   $ 
createFile��$��新建��  
NewCommand# �  m� $��编辑��.@�m� -   $ 	sortWords��$��排序单词��m� -   $ sortByPaging��$��按分页排序��m� -   $ rename��$��重命名词库���� -	    ��Name$ vocabularyName��$��VocabularyModifier��$��1��$��32(�$��等线)�$��
10,10,10,0q�$��33��$	��Top=�$��2���� $��2��$��0��$��5,5,5,0q�$��总览��$
��auto��$	��Top=�$��24(�� $��3��$��0�� �� $��* ��� $
��auto �.+��� -
    ��Name$ overallList��$
��auto��$��0�� �� SelectedItem �� *SelectedWord+���� .R��� $
��auto������ 	 �� Path$	 Summary�"   DataModifier.AddDeleteButton  -   
  Name$
 WordsAddDelete��"   DataModifier.AddDeleteButton   AddButtonClick$# WordsAddDelete_AddButtonClick��   DeleteButtonClick$&  WordsAddDelete_DeleteButtonClick��   Orientation$ 
Horizontal=�$��1���� $
��Auto=�$
��Auto=�$��3��$��1������ -    ��Name$ wordInspector��$��5q� � DataContext �� *SelectedWord+.+��� $��24(����� *	Summary+��  �� Orientation$ 
Horizontal=�.+��� $��Word:��$��75�� �� Target ��  �� ElementName$ 	wordField���� -    ��Name$ 	wordField��$	��300������ *word+�� $ 
Horizontal=�.+��� $��Paging:��$��75�� �� $ 	pageField���� -   $ 	pageField��$	��300������ *paging+   DataModifier.EditableList     Title$ 音素变形��   Subtitle$[ U如a/an等意思完全相同，词性相同，词根相同（构型相近）的单词�� �� *PhonicChanges+�� $ 
Horizontal=�.+��� $��释义��  $$ AddDeleteButton_AddButtonClick��$' !AddDeleteButton_DeleteButtonClick��$ 
Horizontal=��� -   $ MeaningList������ *
Meanings+���� .R��� .+��� .+��� $��词性���� ���� $ PageBase��$	 Types� �� SelectedValue �� $
	 Type�  $ 词义�� �� $	 Translations� � 	Resources ��%<   &	       &	      &	 
     &	       �� �� �� ��  J� InputBindings ��  �� Command# �   �� 	Modifiers$
 Ctrlf�
 �� Key$ S���� # �  $ 
Ctrl+Shiftf�$ S���� # �  $ N��$
 Ctrlf��� # �   $ O��$
 Ctrlf� J� CommandBindings )  #System.Windows.Input.CommandBinding  -      Command# [   -   # [   -   # [    -   # [ 