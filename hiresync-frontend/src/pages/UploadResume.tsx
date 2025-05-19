import React, { useState } from "react";
import axios from "axios";

const UploadResume = () => {
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [statusMessage, setStatusMessage] = useState("");
  const handleUpload = async () => {
    if (!selectedFile) {
      setStatusMessage("Lütfen bir dosya seçin.");
      return;
    }
  
    // ❗ Dosya tipi kontrolü
    if (selectedFile.type !== "application/pdf") {
      setStatusMessage("Yalnızca PDF dosyası yükleyebilirsiniz.");
      return;
    }
  
    const formData = new FormData();
    formData.append("file", selectedFile);
  
    try {
      const response = await axios.post("/api/jobapplications/upload", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      });
      setStatusMessage("CV başarıyla yüklendi: " + response.data.filePath);
    } catch (error: any) {
      const errMsg = error.response?.data || error.message;
      setStatusMessage("Yükleme sırasında hata oluştu: " + errMsg);
    }
  };
  

  return (
    <div style={{ padding: "20px" }}>
      <h2>📤 CV Yükle</h2>

      <input
        type="file"
        accept=".pdf"
        onChange={(e) => {
          if (e.target.files?.length) setSelectedFile(e.target.files[0]);
        }}
      />
      <br /><br />
      <button onClick={handleUpload}>Yükle</button>
      <p>{statusMessage}</p>
    </div>
  );
};

export default UploadResume;
