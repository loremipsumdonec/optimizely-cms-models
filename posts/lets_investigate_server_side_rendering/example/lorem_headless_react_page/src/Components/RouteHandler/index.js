import { getModel } from "./getModel";
import { RouteHandler as Handler } from "./RouteHandler";

export const RouteHandler = () => {
    return <Handler getModel={getModel} />
}