import { Link } from "react-router-dom"

export function Breadcrumbs({breadcrumbs}) {

    return (
        <nav>
            <ol className="list-reset flex">
                {breadcrumbs?.map((item, index) => {
                    return <li key={index} className="breadcrumbs__item parent mr-1"><Link to={item.url}>{item.text}</Link></li>
                })}
            </ol>
        </nav>
    )
}
