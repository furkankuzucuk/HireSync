import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import "../css/JobListings.css";

interface Department {
  departmentName: string;
}

interface Job {
  jobName: string;
}

interface JobList {
  jobListId: number;
  title: string;
  description: string;
  createDate: string;
  departmentName: string;
  jobName: string;
}


const JobListings = () => {
  const [jobs, setJobs] = useState<JobList[]>([]);
  const [expanded, setExpanded] = useState<{ [key: number]: boolean }>({});
  const navigate = useNavigate();

  useEffect(() => {
    const fetchJobs = async () => {
      try {
        const token = localStorage.getItem("token");
        const response = await axios.get("/api/joblists", {
          headers: { Authorization: `Bearer ${token}` },
        });
        setJobs(response.data);
      } catch (error) {
        console.error("İş ilanları alınamadı:", error);
      }
    };

    fetchJobs();
  }, []);

  const handleApply = async (jobListId: number) => {
    try {
      const token = localStorage.getItem("token");
      const username = localStorage.getItem("username");

      if (!username) {
        alert("Kullanıcı adı bulunamadı.");
        return;
      }

      const resumePath = `/uploads/${username}_cv.pdf`;

      const application = {
        jobListId,
        appDate: new Date().toISOString(),
        resumePath,
        status: "Başvuru Yapıldı",
      };

      await axios.post("/api/jobapplications", application, {
        headers: { Authorization: `Bearer ${token}` },
      });

      alert("Başvuru başarıyla yapıldı.");
    } catch (error: any) {
      console.error("Başvuru yapılamadı:", error);

      if (error.response && error.response.status === 400) {
        const message = error.response.data;
        if (typeof message === "string" && message.includes("CV")) {
          alert(message);
          navigate("/candidate-dashboard/upload-resume");
          return;
        }
      }

      alert("Başvuru sırasında bir hata oluştu.");
    }
  };

  const toggleExpand = (id: number) => {
    setExpanded((prev) => ({ ...prev, [id]: !prev[id] }));
  };

  return (
    <div className="job-listings container py-4">
      <h2 className="mb-4 text-primary fw-bold">📌 Açık Pozisyonlar</h2>

      {jobs.map((job) => (
        <div key={job.jobListId} className="card mb-4 shadow-sm">
          <div className="card-body">
            <h5 className="card-title fw-bold">{job.title}</h5>
            <p className={`card-text ${!expanded[job.jobListId] ? "clamp-text" : ""}`}>
              {job.description}
            </p>
            {job.description.split(" ").length > 20 && (
              <button
                className="btn btn-link btn-sm px-0"
                onClick={() => toggleExpand(job.jobListId)}
              >
                {expanded[job.jobListId] ? "Gizle" : "Devamını Oku"}
              </button>
            )}

<p><strong>Departman:</strong> {job.departmentName ?? "Bilinmiyor"}</p>
<p><strong>Pozisyon:</strong> {job.jobName ?? "Bilinmiyor"}</p>


            <p><strong>Yayın Tarihi:</strong> {new Date(job.createDate).toLocaleDateString()}</p>
            <button className="btn btn-success mt-2" onClick={() => handleApply(job.jobListId)}>
              Başvur
            </button>
          </div>
        </div>
      ))}
    </div>
  );
};

export default JobListings;
