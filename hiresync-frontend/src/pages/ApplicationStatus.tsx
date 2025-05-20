import React, { useEffect, useState } from "react";
import axios from "axios";
import "../css/ApplicationStatus.css";

interface Job {
  jobName: string;
}

interface Department {
  departmentName: string;
}

interface JobList {
  title: string;
  job: Job;
  department: Department;
}

interface JobApplication {
  jobApplicationId: number;
  jobListId: number;
  title?: string;
  departmentName?: string;
  jobName?: string;
  jobList?: JobList;
  status: string;
  appDate: string;
}

const ApplicationStatus: React.FC = () => {
  const [applications, setApplications] = useState<JobApplication[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchApplications = async () => {
      try {
        const token = localStorage.getItem("token");
        const userId = localStorage.getItem("userId");

        if (!token || !userId) {
          console.error("Token veya kullanıcı ID'si eksik.");
          return;
        }

        const response = await axios.get(`/api/jobapplications/candidate/${userId}`, {
          headers: { Authorization: `Bearer ${token}` },
        });

        setApplications(response.data);
      } catch (error) {
        console.error("Başvuru verileri alınamadı:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchApplications();
  }, []);

  return (
    <div className="application-status container mt-4">
      <h2 className="mb-4">📄 Başvuru Durumlarım</h2>

      {loading ? (
        <p>Yükleniyor...</p>
      ) : applications.length === 0 ? (
        <p>Hiç başvurunuz bulunmamaktadır.</p>
      ) : (
        <table className="table table-bordered table-striped">
          <thead className="table-dark">
            <tr>
              <th>İlan Başlığı</th>
              <th>Departman</th>
              <th>Pozisyon</th>
              <th>Durum</th>
              <th>Başvuru Tarihi</th>
            </tr>
          </thead>
          <tbody>
            {applications.map((app) => (
              <tr key={app.jobApplicationId}>
                <td>{app.title || app.jobList?.title || "N/A"}</td>
                <td>{app.departmentName || app.jobList?.department?.departmentName || "N/A"}</td>
                <td>{app.jobName || app.jobList?.job?.jobName || "N/A"}</td>
                <td>{app.status}</td>
                <td>{new Date(app.appDate).toLocaleDateString()}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default ApplicationStatus;
