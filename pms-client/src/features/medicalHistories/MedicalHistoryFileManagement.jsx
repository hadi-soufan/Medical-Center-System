import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import FileInput from '../../ui/FileInput';
import Button from '../../ui/Button';
import MiniSpinner from '../../ui/SpinnerMini';

function MedicalHistoryFileManagement() {
  const [photos, setPhotos] = useState([]);
  const [file, setFile] = useState(null);
  const [uploading, setUploading] = useState(false);
  const [userId, setUserId] = useState(null);
  const { id } = useParams(); 

  useEffect(() => {
    async function fetchMedicalHistory() {
      try {
        const response = await axios.get(`http://localhost:5000/api/medicalhistory/${id}`);
        if (response.status === 200) {
          const medicalHistory = response.data;
          setUserId(medicalHistory.patient.userId);
        } else {
          throw new Error('Failed to fetch medical history with status: ' + response.status);
        }
      } catch (error) {
        console.error("Failed to fetch medical history:", error);
      }
    }

    fetchMedicalHistory();
  }, [id]);

  useEffect(() => {
    async function fetchPhotos() {
      if (!userId) return; 
      try {
        const response = await axios.get(`http://localhost:5000/api/photo/user/${userId}`);
        if (response.status === 200) {
          setPhotos(response.data.$values);
        } else {
          throw new Error('Failed to fetch photos with status: ' + response.status);
        }
      } catch (error) {
        console.error("Failed to fetch photos:", error);
      }
    }

    fetchPhotos();
  }, [userId]);

  const handleFileChange = (event) => {
    setFile(event.target.files[0]);
  };

  const handleUpload = async () => {
    if (!file || !userId) return;

    const formData = new FormData();
    formData.append('File', file);
    formData.append('UserId', userId);

    setUploading(true);

    try {
      const response = await axios.post('http://localhost:5000/api/photo/', formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      });

      setUploading(false);

      if (response.data.success) {
        setPhotos(prevPhotos => [...prevPhotos, response.data.photo]);
      } else {
        throw new Error('Upload failed');
      }
    } catch (error) {
      console.error("Error uploading file:", error);
      setUploading(false);
    }
  };

  return (
    <div>
      <h2>User Photos</h2>
      <FileInput id="image" accept="image/*" type="file" onChange={handleFileChange} />
      <Button onClick={handleUpload} disabled={uploading}>
        {uploading ? <MiniSpinner /> : 'Upload Photo'}
      </Button>
      <div style={{ display: 'flex', flexWrap: 'wrap', gap: '10px' }}>
        {photos.map((photo, index) => (
          <div key={index} style={{ border: '1px solid #ccc', padding: '10px' }}>
            <img
              src={photo.url}
              alt="pic"
              style={{ width: '100px', height: '100px' }}
              onClick={() => window.open(photo.url, '_blank')}
            />
          </div>
        ))}
      </div>
    </div>
  );
}

export default MedicalHistoryFileManagement;
