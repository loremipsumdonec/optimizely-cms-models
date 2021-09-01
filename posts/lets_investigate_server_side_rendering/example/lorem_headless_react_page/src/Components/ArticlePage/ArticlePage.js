import Breadcrumbs from "../Breadcrumbs";
import DefaultLayout from "../DefaultLayout/DefaultLayout";

function ArticlePage({heading, preamble, text}) {
    return (
        <DefaultLayout>
            <Breadcrumbs/>
            <h1 className="text-5xl mb-4 font-semibold">{heading}</h1>
            <p className="text-2xl font-medium mb-10">{preamble}</p>
            <div className="article" dangerouslySetInnerHTML={{__html: text}}/>
        </DefaultLayout>
    );
}

export default ArticlePage;