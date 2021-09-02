import DefaultLayout from "../DefaultLayout/DefaultLayout";
import Articles from "../Articles";

function StartPage({heading, preamble, articles}) {
    return (
        <DefaultLayout>
            <h1 className="text-5xl mb-4 font-semibold">{heading}</h1>
            <p className="text-2xl font-medium mb-10">{preamble}</p>
            <Articles articles={articles}/>
        </DefaultLayout>
    );
}

export default StartPage;