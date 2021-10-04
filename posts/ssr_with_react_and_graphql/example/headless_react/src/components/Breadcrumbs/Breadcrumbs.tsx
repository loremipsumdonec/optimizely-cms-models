import { Link } from 'react-router-dom';

interface Breadcrumb {
    text:string,
    url:string
}

interface Props {
    breadcrumbs: Breadcrumb[]
}

export const Breadcrumbs:React.FC<Props> = ({breadcrumbs}) => {
    return (
        <div>
            {breadcrumbs.map(b =>
                <li><Link to={b.url}>{b.text}</Link></li>
            )}
        </div>
    );
}

export default Breadcrumbs;