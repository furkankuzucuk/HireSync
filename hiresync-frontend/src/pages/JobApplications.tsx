import React, { useEffect, useState } from "react";
import axios from "axios";

interface JobApplication {
  jobApplicationId: number;
  jobListId: number;
  userId: number;
  appDate: string;
  status: string;
  resumePath: string;
  userFullName: string;
  jobName: string;
}

const statusOptions = [
  "Başvuru Yapıldı",
  "Değerlendiriliyor",
  "Onaylandı",
  "Reddedildi"
];

const JobApplications: React.FC = () => {
  const [applications, setApplications] = useState<JobApplication[]>([]);
  const [selectedPdfFilename, setSelectedPdfFilename] = useState<string | null>(null);

  // ✅ Token'ı localStorage'dan alıyoruz
  const token = localStorage.getItem("token");

  useEffect(() => {
    if (!token) {
      console.error("Token bulunamadı. Giriş yapmış mısınız?");
      return;
    }

    axios
      .get<JobApplication[]>("/api/jobapplications", {
        headers: {
          Authorization: `Bearer ${token}`
        }
      })
      .then((res) => setApplications(res.data))
      .catch((err) => console.error("Veri alınamadı:", err));
  }, [token]);

  const handleStatusChange = (id: number, newStatus: string) => {
    setApplications((prev) =>
      prev.map((app) =>
        app.jobApplicationId === id ? { ...app, status: newStatus } : app
      )
    );
  };

  const updateStatus = async (id: number, newStatus: string) => {
    if (!token) {
      alert("Yetkisiz işlem. Giriş yapmanız gerekiyor.");
      return;
    }

    try {
      await axios.put(
        `/api/jobapplications/${id}`,
        { status: newStatus },
        {
          headers: {
            Authorization: `Bearer ${token}`
          }
        }
      );
      alert("Durum güncellendi.");
    } catch (error: any) {
      console.error("Durum güncelleme hatası:", error.response?.data || error);
      alert("Güncelleme sırasında hata oluştu.");
    }
  };

  return (
    <div className="container">
      <h2 className="mb-4">İş Başvuruları</h2>

      <table className="table table-bordered table-striped">
        <thead className="table-dark">
          <tr>
            <th>#</th>
            <th>Ad Soyad</th>
            <th>Pozisyon</th>
            <th>Başvuru Tarihi</th>
            <th>Durum</th>
            <th>Güncelle</th>
            <th>CV</th>
          </tr>
        </thead>
        <tbody>
          {applications.map((app, index) => (
            <tr key={app.jobApplicationId}>
              <td>{index + 1}</td>
              <td>{app.userFullName}</td>
              <td>{app.jobName}</td>
              <td>{new Date(app.appDate).toLocaleString()}</td>
              <td>{app.status}</td>
              <td>
                <select
                  value={app.status}
                  onChange={(e) =>
                    handleStatusChange(app.jobApplicationId, e.target.value)
                  }
                  className="form-select form-select-sm"
                >
                  {statusOptions.map((status) => (
                    <option key={status} value={status}>
                      {status}
                    </option>
                  ))}
                </select>
                <button
                  className="btn btn-sm btn-success mt-1"
                  onClick={() =>
                    updateStatus(app.jobApplicationId, app.status)
                  }
                >
                  Güncelle
                </button>
              </td>
              <td>
                <button
                  className="btn btn-sm btn-outline-primary"
                  onClick={() =>
                    setSelectedPdfFilename(app.resumePath.split("/").pop() || "")
                  }
                  data-bs-toggle="modal"
                  data-bs-target="#pdfModal"
                >
                  PDF Aç
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {/* ✅ Modal: PDF Önizleme (iframe) */}
      <div
        className="modal fade"
        id="pdfModal"
        tabIndex={-1}
        aria-labelledby="pdfModalLabel"
        aria-hidden="true"
      >
        <div className="modal-dialog modal-xl modal-dialog-centered modal-fullscreen-sm-down">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title" id="pdfModalLabel">CV Görüntüle</h5>
              <button
                type="button"
                className="btn-close"
                data-bs-dismiss="modal"
                aria-label="Kapat"
              ></button>
            </div>
            <div className="modal-body p-0">
              {selectedPdfFilename ? (
                <iframe
                  src={`http://localhost:5065/uploads/${selectedPdfFilename}`}
                  width="100%"
                  height="700px"
                  title="PDF Preview"
                  style={{ border: "none" }}
                />
              ) : (
                <p>PDF yüklenemedi.</p>
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default JobApplications;
