import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.util.List;
import java.util.Random;
import java.util.UUID;
import java.util.Vector;
import java.util.concurrent.ThreadLocalRandom;

import org.apache.poi.xslf.usermodel.XMLSlideShow;
import org.apache.poi.xslf.usermodel.XSLFSlide;
import org.eclipse.swt.SWT;
import org.eclipse.swt.widgets.FileDialog;
import org.eclipse.swt.widgets.MessageBox;
import org.eclipse.swt.widgets.Shell;

public class CreatePPTAfterShuffle{

	private final String MessageTitle_Info = "Information";
	private final String MessageTitle_Warning = "Warning";
	private final int MessageType_Info = SWT.ICON_INFORMATION;
	private final int MessageType_Warning = SWT.ICON_WARNING;
	
	String tempFolder = null;
	
	public static void main(String[] args) {
		new CreatePPTAfterShuffle().startEditPPTX();
	}
	
	private void startEditPPTX(){
		try{
			String startMessage = "순서 섞는 프로그램 \n에러는 kbj0109@gmail.com 으로 알려주시면 수정 하겠습니다.\n"+"created by Jay BumJun Kim";
			String completeMessage = "셔플파일 생성 완료";
			
			showMessage(MessageType_Info, startMessage);
			 
			String inputFileAddress = selectFileToEdit();
			String outputFileAddress = defineFileNameToSave();
			
			createFile(inputFileAddress, outputFileAddress);
			
			showMessage(MessageType_Info, completeMessage);
			
		}catch(NullPointerException e){
			return;
		}catch(Exception e){
			e.printStackTrace();
			showMessage(MessageType_Warning, "에러 발생으로 생성 실패");
			return;
		}
	}
	
	private String selectFileToEdit() throws NullPointerException{
		showMessage(MessageType_Info, "셔플할 PPTX 파일을 선택 해주세요");
        
        Shell shell = new Shell();
		String[] filterExtension = { "*.pptx" };
		
		FileDialog fileDialog = new FileDialog(shell, SWT.OPEN);
		fileDialog.setFilterExtensions(filterExtension);
		fileDialog.setText("셔플할 PPTX 파일을 선택 해주세요");
		String fileNameWithPath = null;
		try{
			while(true){
				fileNameWithPath = fileDialog.open();
				File file = new File(fileNameWithPath);
				if ( !file.exists() || file.isDirectory()) {
					showMessage(MessageType_Warning, "존재하는 .pptx 파일을 선택 해주세요");
				}else{
					break;
				}
			}
		}catch(NullPointerException e){
			throw new NullPointerException();
		}catch(Exception e){
			e.printStackTrace();
		}
		return fileNameWithPath;
	}
	
	private String defineFileNameToSave() throws NullPointerException{
		showMessage(MessageType_Info, "저장할 위치와 파일 이름을 입력 해주세요");
		
		Shell shell = new Shell();
		String[] filterExtension = { "*.pptx" };
		
		FileDialog fileDialog = new FileDialog(shell, SWT.SAVE);
		fileDialog.setFilterExtensions(filterExtension);
		fileDialog.setText("저장할 위치와 파일 이름을 입력 해주세요");
		String fileNameWithPath = null;
		try{
			while(true){
				fileNameWithPath = fileDialog.open();
				File file = new File(fileNameWithPath);
				if ( file.exists() && !file.isDirectory()) {
					showMessage(MessageType_Warning, "선택한 경로에 파일이 이미 존재합니다");
				}else{
					break;
				}
			}
		}catch(NullPointerException e){
			throw new NullPointerException();
		}catch(Exception e){
			e.printStackTrace();
		}
		return fileNameWithPath;
	}

	//파일 섞고, 만들기 위해 시작하는 메소드
	private void createFile(String inputAddress, String outputAddress) {
		//작동 후 지울 파일들 리스트를 담는다.
		Vector listToDelete = new Vector();
		
		try{
			//샘플 파일들을 넣을 장소를 마련한다.
			tempFolder = createTempFolder();
			
			// 한번 섞고
			String nameOfSameple = createSample(inputAddress);
			listToDelete.add(nameOfSameple);
			
			//한번 더 섞고.
			String nameOfSameple2 = createSample(nameOfSameple);
			listToDelete.add(nameOfSameple2);
			
			//마지막으로 섞고, 완성된 파일 만든다.	(즉 현재 3번 섞는다)
			createFinalOne(nameOfSameple2, outputAddress);
		}catch(Exception e){
			e.printStackTrace();
		}finally{	//샘플로 만든 파일들 지우기
			for(int a=0; a<listToDelete.size(); a++){
				deleteSampleFile(  (String)listToDelete.get(a)  );
			}
			deleteSampleFile(tempFolder);
		}
	}

	//Temp 폴더 만들기
	private String createTempFolder(){
		File folder = null;
		
		while(true){
			String uuid = UUID.randomUUID()+"";
			folder = new File("C:\\"+uuid);
			
			if(folder.exists()&& folder.isDirectory()){
				continue;
			}else{
				break;
			}
		}
		folder.mkdir();
		return folder.getPath();
	}
	
	//섞은 샘플 파일들 만들기
	private String createSample(String inputAddress) {

		File file = new File(inputAddress);
		String fileName = file.getName();

		String nameOfSample = tempFolder+"\\" + "sample_" + fileName;

		try {
			FileInputStream fileInputStream = new FileInputStream(inputAddress);

			XMLSlideShow ppt = new XMLSlideShow(fileInputStream);
			List list = ppt.getSlides();

			int random[] = new int[list.size()];
			for (int a = 0; a < list.size(); a++) {
				random[a] = a;
			}

			shuffleArray(random);

			for (int a = 0; a < list.size(); a++) {
				ppt.setSlideOrder((XSLFSlide) list.get(a), random[a]);
			}

			FileOutputStream out = new FileOutputStream(nameOfSample);
			ppt.write(out);
			out.close();
			ppt.close();

		} catch (Exception e) {
			e.printStackTrace();
		}
		return nameOfSample;
	}

	//마지막으로 완성형 만드는 메소드
	private void createFinalOne(String nameOfSameple, String outputAddress) {
		try {
			FileInputStream fileInputStream = new FileInputStream(nameOfSameple);

			XMLSlideShow ppt = new XMLSlideShow(fileInputStream);
			List list = ppt.getSlides();

			int random[] = new int[list.size()];
			for (int a = 0; a < list.size(); a++) {
				random[a] = a;
			}

			shuffleArray(random);

			for (int a = 0; a < list.size(); a++) {
				ppt.setSlideOrder((XSLFSlide) list.get(a), random[a]);
			}

			FileOutputStream out = new FileOutputStream(outputAddress);
			ppt.write(out);
			out.close();
			ppt.close();

		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	//뒤섞기 위한 순서 지정
	private void shuffleArray(int[] ar) {
		// If running on Java 6 or older, use `new Random()` on RHS here
		Random rnd = ThreadLocalRandom.current();
		for (int i = ar.length - 1; i > 0; i--) {
			int index = rnd.nextInt(i + 1);
			// Simple swap
			int a = ar[index];
			ar[index] = ar[i];
			ar[i] = a;
		}
	}

	// 샘플 파일 지우기
	private void deleteSampleFile(String nameOfSamepl) {
		File file = new File(nameOfSamepl);
		file.delete();
	}
	
	//메세지 띄우기
	private void showMessage(int messageType, String message){
		Shell shell = new Shell();
		MessageBox messageBox = null;
		if(messageType == MessageType_Info){
			messageBox = new MessageBox(shell, MessageType_Info);
			messageBox.setText(MessageTitle_Info);
		}else{
			messageBox = new MessageBox(shell, MessageType_Warning);
			messageBox.setText(MessageTitle_Warning);
		}
		messageBox.setMessage(message);
        messageBox.open();		
	}
}