import { Button } from "@/components/ui/button";
import {
	Card,
	CardContent,
	CardDescription,
	CardHeader,
	CardTitle,
} from "@/components/ui/card";
import { useNavigate } from "react-router-dom";

export default function Dashboard() {
	const navigate = useNavigate();

	return (
		<div className="min-h-screen flex items-start justify-center p-8">
			<div className="w-full max-w-4xl">
				<Card>
					<CardHeader>
						<CardTitle>Dashboard</CardTitle>
						<CardDescription>Welcome — this is the page you see after login.</CardDescription>
					</CardHeader>
					<CardContent>
						<div className="flex flex-col gap-4">
							<p className="text-sm text-muted-foreground">
								Put your dashboard widgets, quick links, and stats here.
							</p>
							<div className="flex gap-2">
								<Button onClick={() => navigate('/')}>Home</Button>
								<Button variant="outline" onClick={() => navigate('/login')}>Logout</Button>
							</div>
						</div>
					</CardContent>
				</Card>
			</div>
		</div>
	);
}
