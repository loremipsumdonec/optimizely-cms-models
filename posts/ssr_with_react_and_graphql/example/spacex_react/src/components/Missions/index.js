import { Missions as Component } from './Missions';
import {useQuery, gql } from "@apollo/client";

const GET_LAUNCHES_PAST = gql`query GetLaunchesPast($limit:Int!)
{
  launchesPast(limit: $limit) {
    mission_name
  }
}`

const Missions = () => {
    const { loading, error, data } = useQuery(GET_LAUNCHES_PAST, { variables: { limit: 10}});

    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error :(</p>;

    return <Component launches={data.launchesPast} />
}

export default Missions;