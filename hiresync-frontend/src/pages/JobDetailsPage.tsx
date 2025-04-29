import React, { useState, useEffect } from "react";
import axios from "axios";
import "../css/JobDetails.css";

const JobDetailsPage = ({ match }: any) => {
  const [jobDetails, setJobDetails] = useState<any>(null);
  const [cv, setCv] = useState<File | null>(null);

  useEffect(() => {
    const fetchJobDetails = async () => {
      try {
        const response = await axios.get(`http://localhost:5065/api/joblists/${match.params.id}`);
        setJobDetails(response.data);
      } catch (error) {
        console.error("İlan detayları alınamadı:", error);
      }
    };

    fetchJobDetails();
  }, [match.params.id]);

  const handleApply = async () => {
    if (cv) {
      const formData = new FormData();
      formData.append("cv", cv);
      formData.append("jobId", jobDetails.jobListId);

      try {
        await axios.post("http://localhost:5065/api/applications", formData);
        alert("Başvurunuz başarıyla gönderildi!");
      } catch (error) {
        console.error("Başvuru sırasında hata:", error);
      }
    } else {
      alert("Lütfen CV yükleyin!");
    }
  };

  return (
    <div className="job-details-page">
      {jobDetails ? (
        <div>
          <h2>{jobDetails.position}</h2>
          <p><strong>Açıklama:</strong> {jobDetails.description}</p>
          <p><strong>Departman:</strong> {jobDetails.departmentName}</p>

          <div>
            <h3>Başvuru Yap</h3>
            <input
              type="file"
              onChange={(e) => setCv(e.target.files ? e.target.files[0] : null)}
              className="form-control"
            />
            <button onClick={handleApply} className="btn btn-success mt-3">Başvur</button>
          </div>
        </div>
      ) : (
        <p>İlan yükleniyor...</p>
      )}
    </div>
  );
};

export default JobDetailsPage;
