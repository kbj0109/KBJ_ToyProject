import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Component;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.GridLayout;
import java.awt.Toolkit;
import java.awt.datatransfer.Clipboard;
import java.awt.datatransfer.StringSelection;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowAdapter;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.LinkedList;
import java.util.List;

import javax.swing.ButtonGroup;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JRadioButton;
import javax.swing.JScrollPane;
import javax.swing.JTextArea;
import javax.swing.border.EmptyBorder;

import com.google.cloud.translate.Language;
import com.google.cloud.translate.Translate;
import com.google.cloud.translate.Translate.TranslateOption;
import com.google.cloud.translate.TranslateOptions;
import com.google.cloud.translate.Translation;

public class MainApp {

	public UI ui = null;
	public Translate translate = null;
	
	public static void main(String[] args) {
		
		MainApp main = new MainApp();
		main.CreateApp();
	}
	
	public void CreateApp() {
		try {
			translate = TranslateOptions.getDefaultInstance().getService();
		}
		catch(Exception ex){
			System.out.println(ex.toString());
			JOptionPane.showMessageDialog(null, "Access Error on Google Translator");
			System.exit(0);
		}
		
		ui = new UI();
		ui.createMainApp(translate);
	}
}


class UI extends JFrame implements ActionListener{
	
	Translate translate = null;
	
	JRadioButton rbKoreanLeft = null;
	JRadioButton rbVietnamLeft = null;
	JRadioButton rbEnglishLeft = null;
	JRadioButton rbKoreanRight = null;
	JRadioButton rbVietnamRight = null;
	JRadioButton rbEnglishRight = null;
	
	JButton button = null;
	JCheckBox checkBoxClipboard = null;
	JCheckBox checkBoxTranslateThroughEnglish = null;
	JComboBox comboOrigLan = null;
	JComboBox comboDestLan = null;
	JTextArea textLeft = null;
	JTextArea textRight = null;
	
	public void createMainApp(Translate translate) {
		
		this.setLayout(new GridLayout(1,2));
		this.setSize(900, 600);//�겕湲곗� �쐞移� 議곗젅. 	�샊�� Super().setBounds �빐�룄 �맂�떎.
		this.setLocation((Toolkit.getDefaultToolkit().getScreenSize().width  - getSize().width) / 2, (Toolkit.getDefaultToolkit().getScreenSize().height - getSize().height) / 2);
		this.setTitle("Translator");
		
		String iconPath = System.getProperty("user.dir");
		iconPath += "/TK_Icon.jpg";
		ImageIcon img = new ImageIcon(iconPath);
		setIconImage(img.getImage());
		
		
		//�쇊履� �뙣�꼸 遺�遺�
		JPanel panelLeftCenter = new JPanel(new BorderLayout(10, 10));
		panelLeftCenter.setBorder(new EmptyBorder(0,5,0,5) );
		
		comboOrigLan = new JComboBox<String>();
		panelLeftCenter.add(comboOrigLan, "North");
		
		textLeft = new JTextArea();
		textLeft.setLineWrap(true);
		textLeft.setWrapStyleWord(true);
		JScrollPane scrollLeft = new JScrollPane(textLeft);
		panelLeftCenter.add(scrollLeft, "Center");
		
		button = new JButton("Translate");
		button.setPreferredSize(new Dimension(100,40));
		panelLeftCenter.add(button, "South");
		
		
		//�삤瑜몄そ �뙣�꼸 遺�遺�
		JPanel panelRightCenter = new JPanel(new BorderLayout(10,10));
		panelRightCenter.setBorder(new EmptyBorder(0,5,0,5) );
		
		comboDestLan = new JComboBox<String>();
		panelRightCenter.add(comboDestLan, "North");
		
		textRight = new JTextArea();
		//textRight.setEditable(false); // �닔�젙 遺덇�
		textRight.setLineWrap(true);
		textRight.setWrapStyleWord(true);
		JScrollPane scrollRight = new JScrollPane(textRight);
		panelRightCenter.add(scrollRight, "Center");

		
		//�삤瑜몄そ �븯�떒�쑝 CheckBox 紐⑥쓬 �뙣�꼸
		checkBoxClipboard = new JCheckBox("Copy To Clipboard");
		checkBoxClipboard.setPreferredSize(new Dimension(140,40)); //�쇊履� 踰꾪듉怨� �겕湲� 洹좏삎 留욎텛湲� �쐞�빐
		checkBoxTranslateThroughEnglish = new JCheckBox("English Base");
		
		JPanel panelRightBottom = new JPanel(new FlowLayout(0, 0, 0));
		panelRightBottom.add(checkBoxClipboard);
		panelRightBottom.add(checkBoxTranslateThroughEnglish);
		panelRightCenter.add(panelRightBottom, "South");
		
		//�뼇履� Header 遺�遺� 留뚮뱾湲�, RadioButton
		rbKoreanLeft = new JRadioButton("Korean");
		rbVietnamLeft = new JRadioButton("Vietnamese");
		rbEnglishLeft = new JRadioButton("English");
		rbKoreanRight = new JRadioButton("Korean");
		rbVietnamRight = new JRadioButton("Vietnamese");
		rbEnglishRight = new JRadioButton("English");
		
		ButtonGroup groupLeft = new ButtonGroup();
		ButtonGroup groupRight= new ButtonGroup();
		groupLeft.add(rbKoreanLeft);
		groupLeft.add(rbEnglishLeft);
		groupLeft.add(rbVietnamLeft);
		groupRight.add(rbKoreanRight);
		groupRight.add(rbEnglishRight);
		groupRight.add(rbVietnamRight);
		
		JPanel panelLeftHeader = new JPanel(new FlowLayout(FlowLayout.LEFT));
		JPanel panelRightHeader = new JPanel(new FlowLayout(FlowLayout.LEFT));
		panelLeftHeader.add(rbKoreanLeft);
		panelLeftHeader.add(rbEnglishLeft);
		panelLeftHeader.add(rbVietnamLeft);
		panelRightHeader.add(rbKoreanRight);
		panelRightHeader.add(rbEnglishRight);
		panelRightHeader.add(rbVietnamRight);

		
		//Header�� Center瑜�, �븯�굹�쓽 �뙣�꼸�뿉 �꽔湲�. (�쇊履� �삤瑜몄そ 紐⑤몢)
		JPanel panelLeft = new JPanel(new BorderLayout());
		JPanel panelRight = new JPanel(new BorderLayout());
		panelLeft.add(panelLeftCenter, "Center");
		panelLeft.add(panelLeftHeader,"North");
		panelRight.add(panelRightCenter, "Center");
		panelRight.add(panelRightHeader,"North");
		
		
		//留덈Т由�
		this.add(panelLeft);
		this.add(panelRight);
		
		setEvent();//�씠踰ㅽ듃 �꽭�똿
		setComboBoxWithSupportedLanugage(translate); //ComboBox�뿉 吏��썝 �뼵�뼱 由ъ뒪�듃 �꽔湲�
		setDefaultSetting();
		
		this.setVisible(true); // �씠嫄� 留덉�留됱뿉 �빐�빞�븳�떎
		
		this.translate = translate;
	}

	//�씠踰ㅽ듃 �꽕�젙
	public void setEvent() {
		button.addActionListener(this);	//�씠踰ㅽ듃 異붽��븯�뒗 �몢踰덉㎏ 諛⑸쾿�쓣 �쐞�빐 �븘�슂
		rbKoreanLeft.addActionListener(this);
		rbEnglishLeft.addActionListener(this);
		rbVietnamLeft.addActionListener(this);
		rbKoreanRight.addActionListener(this);
		rbEnglishRight.addActionListener(this);
		rbVietnamRight.addActionListener(this);
		
		//Frame 李쎌씠 �떕�븘吏� �븣, 源붾걫�븳 醫낅즺瑜� �쐞�븿
		this.addWindowListener(new java.awt.event.WindowAdapter() {
		    @Override
		    public void windowClosing(java.awt.event.WindowEvent windowEvent) {
		            System.exit(0);
		    }
		});
	}
	
	//ComboBox�뿉 吏��썝 媛��뒫�븳 �뼵�뼱 紐⑸줉 �엯�젰
	public void setComboBoxWithSupportedLanugage(Translate translate) {
		List<Language> list = translate.listSupportedLanguages();
		
		for(int a=0; a<list.size(); a++) {
			Language lan = list.get(a);
			String languageCode = lan.getName() + " (" + lan.getCode()+ ")";
			comboOrigLan.addItem(languageCode);
			comboDestLan.addItem(languageCode);
		}
	}
	
	//湲곕낯 �꽭�똿 - �룿�듃 �궗�씠利�, �꽑�깮�맂 �뼵�뼱
	public void setDefaultSetting() {

		//TextArea�뿉 �룿�듃 �겕湲� �솗��
		Font f = textLeft.getFont();
		Font f2 = new Font(f.getFontName(), f.getStyle(), f.getSize()+2);
		textLeft.setFont(f2);
		textRight.setFont(f2);
		
		//ComboBox�뿉 湲곕낯 �꽑�깮 �뼵�뼱 �꽕�젙
		rbKoreanLeft.setSelected(true);
		rbVietnamRight.setSelected(true);
		selectLanguageOnComboBox(comboOrigLan, "Korean");
		selectLanguageOnComboBox(comboDestLan, "Vietnamese");
		
		checkBoxClipboard.setSelected(true); //踰덉뿭 �썑 �겢由쎈낫�뱶�뿉 �궡�슜 蹂듭궗�븯�뒗 泥댄겕諛뺤뒪 �겢由�.
	}
	
	//JComboBox�뿉�꽌 �듅�젙 �뼵�뼱瑜� 李얠븘�꽌 �꽑�깮�븳�떎
	public void selectLanguageOnComboBox(JComboBox combo, String language) {
		for(int a=0; a< combo.getItemCount(); a++) {
 			if(combo.getItemAt(a).toString().contains(language)) {
 				combo.setSelectedIndex(a);
 			}
 		}
	}
	
	//ComboBox�뿉�꽌 �뼵�뼱 肄붾뱶 媛��졇�삤湲�
	public String getLanguageCodeFromSelectedItem(JComboBox combo) {
		int start = combo.getSelectedItem().toString().lastIndexOf("(");
		int end = combo.getSelectedItem().toString().lastIndexOf(")");
		return combo.getSelectedItem().toString().substring(start+1, end);
	}

	//**** 踰덉뿭�븯湲� *******
	public String TranslateText(String originalText, String orgCode, String destCode) {

		List<Translation> listTranslation = null;//踰덉뿭 �젙蹂대�� 媛�吏� Translation 媛앹껜�쓽 由ъ뒪�듃
		LinkedList<String> listOriginalText = new LinkedList(); //踰덉뿭 �쟾�쓽 湲��옄, Enter�궎 湲곗��쑝濡� �굹�닠吏�.
		LinkedList<Integer> listBlankIndex = new LinkedList<Integer>(); //Enter�궎�쓽 �쐞移�
		
		//癒쇱� 湲곗〈�쓽 硫붿꽭吏�瑜�, 以� �굹�닎(Enter) 湲곗��쑝濡� �굹�늿�떎
		String[] text = originalText.split("\n");
		for(int a=0; a<text.length; a++) {
			if(text[a].trim().equals("")) {
				listBlankIndex.add(a);
			}
			else {
				listOriginalText.add(text[a].trim());
			}
		}
		
		//English Base媛� �겢由��맂 �긽�깭�씠怨�, �꽑�깮�맂 踰덉뿭 �뼵�뼱�뿉 �쁺�뼱媛� �뾾�쓣 �븣, 紐⑤뱺 踰덉뿭�쓣 �쁺�뼱濡� �븳踰� 嫄곗퀜�꽌 踰덉뿭�맂�떎
		if(checkBoxTranslateThroughEnglish.isSelected() && orgCode.contains("en") == false && destCode.contains("en") == false ) {
			//1. 癒쇱� �쁺�뼱濡� 踰덉뿭
			listTranslation = translate.translate(listOriginalText, 
					TranslateOption.sourceLanguage(orgCode), 
					TranslateOption.targetLanguage("en"));	
			
			//2. �떎�떆 �븯�굹濡� �빀移섍퀬
			listOriginalText.clear();
			for(int a=0; a< listTranslation.size(); a++) {
				String halfTranslated = listTranslation.get(a).getTranslatedText().replaceAll("&#39;", " wa").replaceAll("&quot;", "\"");
				listOriginalText.add(halfTranslated);
			}
			
			//3. �썝�븯�뒗 �뼵�뼱濡� 踰덉뿭
			listTranslation = translate.translate(listOriginalText, 
					TranslateOption.sourceLanguage("en"), 
					TranslateOption.targetLanguage(destCode));	
		}
		//English Base媛� �겢由��릺�뼱 �엳吏� �븡�떎硫�, 洹몃깷 諛붾줈 踰덉뿭�븳�떎
		else {
			listTranslation = translate.translate(listOriginalText, 
					TranslateOption.sourceLanguage(orgCode), 
					TranslateOption.targetLanguage(destCode));	
		}
		
		String translatedText = "";
		
		//踰덉뿭�맂 Translation 媛앹껜�뿉�꽌, 硫붿꽭吏�瑜� �씫�뼱�꽌 String ���엯�쓽 List濡� 蹂��솚�븳�떎
		LinkedList<String> listTranslatedText = new LinkedList<String>();
		for(int a =0; a<listTranslation.size(); a++) {
			listTranslatedText.add(listTranslation.get(a).getTranslatedText());
		}
		
		//踰덉뿭�맂 硫붿꽭吏� 由ъ뒪�듃�뿉, �썝�옒 �엳�뜕 Blank Line�쓣 �떎�떆 異붽��븳�떎. (�썝�솢�븳 踰덉뿭�쓣 �쐞�빐 �씪�떆 �젣嫄고뻽�뿀�떎)
		for(int a=0; a<listBlankIndex.size(); a++) {
			//listTranslatedText.add(listBlankIndex.get(a), "\n");
		}
		
		//踰덉뿭�맂 硫붿꽭吏�瑜� �븯�굹�쓽 String�쑝濡� 寃고빀
		for(int a=0; a < listTranslatedText.size(); a++) {
			translatedText += listTranslatedText.get(a) + "\n\n";
		}

		//踰덉뿭�맂 �썑, ["]媛� &quot; 濡� 蹂��븯�뒗 寃껋쓣 諛⑹�
		translatedText = translatedText.replaceAll("&quot;", "\"");
		translatedText = translatedText.replaceAll("&#39;", " wa");
		
		return translatedText;
	}
	
	//媛곸쥌 �씠踰ㅽ듃
	@Override
	public void actionPerformed(ActionEvent e) {
		
		if(e.getSource() == rbKoreanLeft) {
			selectLanguageOnComboBox(comboOrigLan, "Korean");
		}
		if(e.getSource() == rbEnglishLeft) {
			selectLanguageOnComboBox(comboOrigLan, "English");
		}
		if(e.getSource() == rbVietnamLeft) {
			selectLanguageOnComboBox(comboOrigLan, "Vietnamese");
		}
		if(e.getSource() == rbKoreanRight) {
			selectLanguageOnComboBox(comboDestLan, "Korean");
		}
		if(e.getSource() == rbEnglishRight) {
			selectLanguageOnComboBox(comboDestLan, "English");
		}
		if(e.getSource() == rbVietnamRight) {
			selectLanguageOnComboBox(comboDestLan, "Vietnamese");
		}
		
		//Translate 踰꾪듉 �닃�졇�쓣 �븣
		if(e.getSource() == button) {
			String orgCode = getLanguageCodeFromSelectedItem(comboOrigLan);
			String destCode = getLanguageCodeFromSelectedItem(comboDestLan);
			String originalText = textLeft.getText();
			String translatedText = "";
			
			//踰덉뿭�쓣 �쐞�빐 �엯�젰�맂 媛믪씠 �뾾�떎硫� 踰덉뿭 �슂泥� �븞�븿
			String msgForCheck = originalText.replaceAll("\n", "");
			if(msgForCheck.trim().contentEquals("")){
				textRight.setText("");
				return;
			}
			
			try {//踰덉뿭 以� Exception 諛쒖깮�븯硫� 踰덉뿭 以묒� 
				translatedText = TranslateText(originalText, orgCode, destCode); //踰덉뿭�븯湲�
		 		textRight.setText(translatedText);	
			}
	 		catch(Exception ex) {
	 			JOptionPane.showMessageDialog(null, "Exception While Translating");
	 			return;
	 		}

			try {//�겢由쎈낫�뱶�뿉 �궡�슜�쓣 蹂듭궗
				if(checkBoxClipboard.isSelected()) {
					StringSelection selection = new StringSelection(translatedText);
					Clipboard clipboard = Toolkit.getDefaultToolkit().getSystemClipboard();
					clipboard.setContents(selection, selection);
				}	
			}
			catch(Exception ex) {
				JOptionPane.showMessageDialog(null, "Exception on Clipboard");
			}
		}
	}

	
}