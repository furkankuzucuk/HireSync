import React, { useState } from "react";
import axios from "axios";
import "../css/JobPostingManagement.css";

const JobPostingManagement = () => {
  const [description, setDescription] = useState("");
  const [departmentId, setDepartmentId] = useState<number>(2);
  const [successMessage, setSuccessMessage] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const newJob = {
        departmentId: departmentId,
        description: description,
        // CreateDate YOK! Backend kendisi verecek.
      };

      const response = await axios.post("http://localhost:5065/api/joblists", newJob);

      if (response.status === 201) {
        setSuccessMessage("İş ilanı başarıyla yayınlandı!");
        setDescription(""); 
        setDepartmentId(2); 
      }
    } catch (error: any) {
      console.error("İlan yayınlama hatası:", error.response ? error.response.data : error.message);
      setSuccessMessage("Bir hata oluştu: " + (error.response?.data || error.message));
    }
  };

  return (
    <div className="tab-content">
      <h2>📌 İş İlanları Yönetimi</h2>

      {successMessage && (
        <div className="alert alert-success" role="alert">
          {successMessage}
        </div>
      )}

      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label>Departman:</label>
          <select
            className="form-control"
            value={departmentId}
            onChange={(e) => setDepartmentId(parseInt(e.target.value))}
          >
            <option value={2}>Information Technology</option>
            <option value={3}>Bilgi Teknolojileri (IT)</option>
            <option value={4}>İnsan Kaynakları (HR)</option>
            <option value={5}>Satış ve Pazarlama</option>
            <option value={6}>Muhasebe ve Finans</option>
            <option value={7}>Üretim ve Operasyon</option>
            <option value={8}>Müşteri Hizmetleri</option>
            <option value={9}>Kurumsal İletişim ve Halkla İlişkiler (PR)</option>
          </select>
        </div>

        <div className="mb-3">
          <label>İlan Açıklaması:</label>
          <textarea
            className="form-control"
            placeholder="İlan açıklaması"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            required
          />
        </div>

        <button type="submit" className="btn btn-dark">
          İlanı Yayınla
        </button>
      </form>
    </div>
  );
};

export default JobPostingManagement;
