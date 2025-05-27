import React, { useState } from "react";
import axios from "axios";
import "../css/UploadResume.css";

const UploadResume = () => {
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [statusMessage, setStatusMessage] = useState("");

  const handleUpload = async () => {
    if (!selectedFile) {
      setStatusMessage("❗ Lütfen bir dosya seçin.");
      return;
    }

    if (selectedFile.type !== "application/pdf") {
      setStatusMessage("❌ Yalnızca PDF dosyası yükleyebilirsiniz.");
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

      setStatusMessage("✅ CV başarıyla yüklendi.");
    } catch (error: any) {
      const errMsg = error.response?.data || error.message;
      setStatusMessage("❌ Yükleme sırasında hata oluştu: " + errMsg);
    }
  };

  return (
    <div className="upload-resume-container">
      <div className="upload-box shadow">
        <h2 className="text-center mb-4">📤 CV Yükle</h2>

        <input
          type="file"
          accept=".pdf"
          className="form-control mb-3"
          onChange={(e) => {
            if (e.target.files?.length) setSelectedFile(e.target.files[0]);
          }}
        />

        <button className="btn btn-primary w-100 mb-2" onClick={handleUpload}>
          Yükle
        </button>

        {statusMessage && (
          <div className="alert alert-info text-center mt-3" role="alert">
            {statusMessage}
          </div>
        )}
      </div>
    </div>
  );
};

export default UploadResume;
