
import { useEffect,useState } from 'react';

function App()
{
  const[employees,setEmployes]=useState([]);
  const[name,setName]=useState("");
  const[position,setPosition]=useState("");
  const API_URL = "https://ajit-employee-api-fkfhezhfgtatc5dv.centralindia-01.azurewebsites.net/api/employee";

  const fetchEmployees=async()=>{
  const res=await fetch(API_URL);
  const data=await res.json();
  setEmployes(data);
  };
  useEffect(()=>{
    fetchEmployees();
  },[]);

  const handleSubmit = async(e)=>{
    e.preventDefault();
    await fetch(API_URL,
      {
        method:"POST",
        headers:{
          "content-Type":"application/json"
        },
        body:JSON.stringify(
          {
            name,position
          }
        )

      }
    );
    setName("");
    setPosition("");
    fetchEmployees();
  };

  return(
    <div style={{padding:"20px"}}>
 <h2>
Employee App
    </h2>
    <form onSubmit={handleSubmit}>
      <input type='text' placeholder="Name" value={name} onChange={(e)=>setName(e.target.value)}/>
      <input type='text' placeholder="Position" value={position} onChange={(e)=>setPosition(e.target.value)}/>
      <button type='submit'>Add</button>
    </form>
    <ul>
      {employees.map((emp)=>(
        <li key={emp.id}>
           {emp.name} - {emp.position}
        </li>
      ))}
    </ul>
    </div>
   
  )
}

export default App;
