import React from 'react';

export const Missions = ({launches}) => {

    return(
        <div>
            <h2>Missions</h2>
            <ul>
                {launches?.map((l, index) => 
                    <li key={index}>{l.mission_name}</li>
                )}
            </ul>
        </div>
    );
}

export default Missions;