import { useSelector } from 'react-redux'
import { Breadcrumbs as Component} from './Breadcrumbs'

const Breadcrumbs = () => {

    const breadcrumbs = useSelector(state => state.model.model.breadcrumbs);

    return(
        <Component {...breadcrumbs}/>
    )
}

export default Breadcrumbs