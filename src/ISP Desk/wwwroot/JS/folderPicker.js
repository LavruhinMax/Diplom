

async function showFolderPicker() {

        if ('showDirectoryPicker' in window) {
            const dirHandle = await window.showDirectoryPicker();
            return dirHandle.name;
        } else {
            const input = document.createElement('input');
            input.type = 'file';
            input.webkitdirectory = true;
            input.style.display = 'none';
            document.body.appendChild(input);
            
            return new Promise((resolve) => {
                input.addEventListener('change', () => {
                    if (input.files.length > 0) {
                        const path = input.files[0].webkitRelativePath;
                        const folderName = path.split('/')[0];
                        resolve(folderName);
                    } else {
                        resolve('');
                    }
                    document.body.removeChild(input);
                });
                input.click();
            });
        }
    
}